using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Data;
using Users.API.Models.Domain;
using Users.API.Models.DTO;
using Users.API.Repositories.Interface;

namespace Users.API.Controllers
{
    // https://localhost:xxxx:/api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISkillsetRepository skillsetRepository;
  

        public UserController(IUserRepository userRepository, ISkillsetRepository skillsetRepository)
        {
            this.userRepository = userRepository;
            this.skillsetRepository = skillsetRepository;
        }
        // POST: https://localhost:xxxx:/api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestDTO request) 
        {
            if (request == null)
            {
                return BadRequest("Invalid blog post data");
            }
            // Inline Validation (Optional but recommended)
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Username and Email.");
            }
            // be elegant, be disciplined
            try
            {
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Hobby = request.Hobby,
                    RegisterDate = request.RegisterDate,
                    isVisible = request.isVisible,

                    Skillset = new List<Skillset>()
                };

                foreach (var skillsetId in request.Skillset)
                {
                    var existingSkillset = await skillsetRepository.GetSkillsetById(skillsetId);
                    if (existingSkillset != null)
                    {
                        user.Skillset.Add(existingSkillset);
                    }
                }

                await userRepository.CreateUserAsync(user); // <--- Saving it here

                if (user != null)
                {
                    // model to dto
                    var abstractedResponse = new UserDTO
                    {
                        UserId = user.UserId,
                        Username = request.Username,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Hobby = request.Hobby,
                        RegisterDate = request.RegisterDate,
                        isVisible = request.isVisible,
                        Skillset = user.Skillset.Select(x => new SkillsetDTO
                        {
                            SkillsetId = x.SkillsetId,
                            SkillName = x.SkillName,
                        }).ToList(), // <--- NEED TO FIX! maybe? works ok lol
                    };

                    return Ok(abstractedResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }
            return BadRequest(request);
        }

    }
}
