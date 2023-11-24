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
                    ProfileImageUrl = request.ProfileImageUrl,

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
                        ProfileImageUrl = request.ProfileImageUrl,
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

        // GET: https://localhost:xxxx:/api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            try
            {
                // fetch the users using repo
                var users = await userRepository.GetAllUsersAsync();


                // Check if the users list is empty
                if (users == null || !users.Any())
                {
                    return NotFound("No users found.");
                }
                // instantize an empty list to hold your response data in a list
                var response = new List<UserDTO>();

                // use for loop to go initialize data in response using data from users

                foreach (var user in users)
                {
                    response.Add(new UserDTO
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Hobby = user.Hobby,
                        RegisterDate = user.RegisterDate,
                        isVisible = user.isVisible,

                        // skillset is needs to be mapped to its own DTO
                        Skillset = user.Skillset.Select(x => new SkillsetDTO
                        {
                            SkillsetId = x.SkillsetId,
                            SkillName = x.SkillName,
                        }).ToList(),
                    });
                }

                // return the dto response
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (consider adding logging if not already in place)
                // _logger.LogError(ex, "Error retrieving all users.");

                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }


        }

        // GET: https://localhost:xxxx:/api/user/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {

            // Check if the provided ID is valid
            if (id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            try
            {
                // fetch blogpost using repo
                var existingUser = await userRepository.GetUserById(id);

                if (existingUser == null)
                {
                    return NotFound("User wasnt found in database, maybe they need to register");
                }
                // intialize response object in form of dto

                var response = new UserDTO
                {
                    UserId = existingUser.UserId,
                    Username = existingUser.Username,
                    Email = existingUser.Email,
                    PhoneNumber = existingUser.PhoneNumber,
                    Hobby = existingUser.Hobby,
                    RegisterDate = existingUser.RegisterDate,
                    isVisible = existingUser.isVisible,
                    ProfileImageUrl = existingUser.ProfileImageUrl,

                    // skillset is needs to be mapped to its own DTO
                    Skillset = existingUser.Skillset.Select(x => new SkillsetDTO
                    {
                        SkillsetId = x.SkillsetId,
                        SkillName = x.SkillName,
                    }).ToList(),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (consider implementing logging if not already in place)
                // _logger.LogError(ex, "Error retrieving user with ID: {id}", id);

                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
            
        }
        // PUT: https://localhost:xxxx:/api/user/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequestDTO request)
        {

            // Check if the provided ID is valid
            if (id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            // Validate request data
            if (request == null)
            {
                return BadRequest("Update data is required.");
            }

            // init domain model, convert to dto remember

            try
            {
                var newUser = new User
                {
                    UserId = id,
                    Username = request.Username,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Hobby = request.Hobby,
                    RegisterDate = request.RegisterDate,
                    isVisible = request.isVisible,
                    ProfileImageUrl = request.ProfileImageUrl,
                    Skillset = new List<Skillset>(),
                };

                foreach (var skillset in request.Skillset)
                {
                    var existingSkillset = await skillsetRepository.GetSkillsetById(skillset);
                    if (existingSkillset == null)
                    {
                        return BadRequest("Could not find id in skillset table.");
                    }
                    newUser.Skillset.Add(existingSkillset);
                }

                var updatedUser = await userRepository.UpdateAsync(newUser);
                if (updatedUser == null)
                {
                    return NotFound("Could not update user. The user may not exist.");
                }

                var response = new UserDTO
                {
                    UserId = newUser.UserId,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    Hobby = newUser.Hobby,
                    RegisterDate = newUser.RegisterDate,
                    isVisible = newUser.isVisible,
                    ProfileImageUrl = newUser.ProfileImageUrl,
                    Skillset = newUser.Skillset.Select(x => new SkillsetDTO
                    {
                        SkillsetId = x.SkillsetId,
                        SkillName = x.SkillName,
                    }).ToList(),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception here if logging is set up
                // _logger.LogError(ex, "Error updating user with ID: {id}", id);

                return StatusCode(500, "An error occurred while updating the user: " + ex.Message);
            }
        }

        // DELETE: https://localhost:xxxx:/api/user/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            try
            {
                // call repo method straight away, and then map into dto and send
                var delUser = await userRepository.DeleteAsync(id);

                if (delUser == null)
                {
                    return NotFound("Couldnt delete corresponding id as user with that id isnt found");
                }

                var response = new UserDTO
                {
                    UserId = delUser.UserId,
                    Username = delUser.Username,
                    Email = delUser.Email,
                    PhoneNumber = delUser.PhoneNumber,
                    Hobby = delUser.Hobby,
                    RegisterDate = delUser.RegisterDate,
                    isVisible = delUser.isVisible,
                    ProfileImageUrl = delUser.ProfileImageUrl,

                    // skillset is needs to be mapped to its own DTO
                    Skillset = delUser.Skillset.Select(x => new SkillsetDTO
                    {
                        SkillsetId = x.SkillsetId,
                        SkillName = x.SkillName,
                    }).ToList(),
                };

                return Ok(new { message = "Deleted successfully", user = response });
                // <--- CHANGE THIS TO 204 NOCONTENT LATER
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error deleting user with ID: {Id}", id);

                return StatusCode(500, "An error occurred while attempting to delete the user: " + ex.Message);
            }
            
        }
    }
}
