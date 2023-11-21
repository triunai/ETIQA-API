using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Repositories.Interface;
using Users.API.Models.DTO;
using Users.API.Models.Domain;

namespace Users.API.Controllers
{
    // https://localhost:7044/api/skillset
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsetController : ControllerBase
    {
        private readonly ISkillsetRepository skillsetRepository;
        public SkillsetController(ISkillsetRepository skillsetRepository)
        {
            this.skillsetRepository = skillsetRepository;
        }

        // to create a new skillset
        // POST: https://localhost:7044/api/skillset
        [HttpPost]
        public async Task<IActionResult> CreateSkillset([FromBody] CreateSkillsetRequestDTO request)
        {
            // Check if the request body is null
            if (request == null)
            {
                return BadRequest("Skillset data is required.");
            }

            // Inline Validation
            if (string.IsNullOrEmpty(request.SkillName))
            {
                return BadRequest("Skill Name is required.");
            }

            try
            {
                // Create a new domain model with dto values
                var skillset = new Skillset
                {
                    SkillName = request.SkillName
                };

                // Writes to database, saves changes, and returns the saved skillset
                await skillsetRepository.CreateSkillsetAsync(skillset);

                if(skillset != null)
                {
                    // Create a response object from domain model in DTO format
                    var response = new SkillsetDTO
                    {
                        SkillsetId = skillset.SkillsetId,
                        SkillName = skillset.SkillName
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Couldnt make the skillset");
                }
                
            }
            catch (Exception ex)
            {
                // Log the exception details (if logging is in place)
                // Log.Error(ex, "Error creating skillset");

                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET ALL : https://localhost:7044/api/skillset
        [HttpGet]
        public async Task<IActionResult> GetAllSkillset()
        {
            try
            {
                var skillsets = await skillsetRepository.GetAllSkillsetAsync();

                // Check if the skillsets list is empty
                if (skillsets == null || !skillsets.Any())
                {
                    return NotFound("No skillsets found.");
                }

                // Use LINQ to transform the domain models to DTOs
                var response = skillsets.Select(skillset => new SkillsetDTO
                {
                    SkillsetId = skillset.SkillsetId,
                    SkillName = skillset.SkillName
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error retrieving all skillsets.");

                return StatusCode(500, "An error occurred while processing your request: " + ex);
            }
        }

        // GET: https://localhost:7044/api/Skillset/{Guid}
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetSkillset(Guid Id)
        {
            // Check if the provided ID is valid
            if (Id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            try
            {
                var existingSkillset = await skillsetRepository.GetSkillsetById(Id);

                if (existingSkillset == null)
                {
                    return NotFound("Skillset not found in the database.");
                }

                var response = new SkillsetDTO
                {
                    SkillsetId = existingSkillset.SkillsetId,
                    SkillName = existingSkillset.SkillName
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error retrieving skillset with ID: {Id}", id);

                return StatusCode(500, "An error occurred while processing your request: "+ex);
            }
        }


        // DELETE: https://localhost:7044/api/skillset/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSkillset(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            var skillsetToDelete = await skillsetRepository.GetSkillsetById(id);
            if (skillsetToDelete == null)
            {
                return NotFound("Skillset not found and cannot be deleted.");
            }

            try
            {
                var skillset = await skillsetRepository.DeleteAsync(id);
                if (skillset == null)
                {
                    return NotFound("Data cant be deleted because it wasnt found");
                }

                var response = new SkillsetDTO
                {
                    SkillsetId = skillset.SkillsetId,
                    SkillName = skillset.SkillName
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error deleting skillset with ID: {Id}", id);

                return StatusCode(500, "An error occurred while attempting to delete the skillset: "+ex);
            }

        }


        // PUT: https://localhost:7044/api/skillset/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSkillset(Guid id, [FromBody] UpdateSkillsetRequestDTO request)
        {
            // Check if the provided ID is valid
            if (id == Guid.Empty)
            {
                return BadRequest("A valid GUID must be provided.");
            }

            // Validate request data
            if (request == null || string.IsNullOrEmpty(request.SkillName))
            {
                return BadRequest("Skill Name is required.");
            }

            try
            {
                // Check if the skillset exists before updating
                var existingSkillset = await skillsetRepository.GetSkillsetById(id);
                if (existingSkillset == null)
                {
                    return NotFound("Skillset not found.");
                }

                // Update properties of the existing skillset
                existingSkillset.SkillName = request.SkillName;

                // Update the skillset in the repository
                await skillsetRepository.UpdateAsync(existingSkillset);

                // Form a response DTO based on the updated model
                var response = new SkillsetDTO
                {
                    SkillsetId = existingSkillset.SkillsetId,
                    SkillName = existingSkillset.SkillName
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error updating skillset with ID: {Id}", id);

                return StatusCode(500, "An error occurred while updating the skillset: "+ex);
            }
        }

    }
}
