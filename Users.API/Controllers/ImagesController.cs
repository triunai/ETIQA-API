using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Data;
using Users.API.Models.Domain;
using Users.API.Models.DTO;
using Users.API.Repositories.Interface;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ApplicationDbContext dbContext;
        public ImagesController(IImageRepository imageRepository,
                            ApplicationDbContext dbContext)
        {
            this.imageRepository = imageRepository;
            this.dbContext = dbContext;
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format!");
            }

            if (file.Length > 10485760) // more than 10 mb
            {
                ModelState.AddModelError("file", "File size cannot be more than 10mb");
            }
        }

        [HttpPost] // using FROMFORM commonly used in POST
        public async Task<IActionResult> UploadImage(
      [FromForm] IFormFile file,
      [FromForm] string fileName,
      [FromForm] string title)
        {
            if (file == null)
            {
                return NotFound("File not found! try again");
            }

            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                // initializing domain model remember to change it later into DTO
                var blogImage = new ProfileImage
                {
                    Title = title,
                    DateCreated = DateTime.Now,
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                };
                var image = await imageRepository.Upload(file, blogImage);

                if (image != null)
                {
                    // conversion to dto
                    var response = new ProfileImageDTO
                    {
                        Title = image.Title,
                        DateCreated = DateTime.Now,
                        FileExtension = image.FileExtension,
                        FileName = image.FileName,
                        FileUrl = image.FileUrl, // build from the repo

                    };
                    return Ok(response);
                }
                return BadRequest("Image was null");

            }
            return BadRequest("model state wasnt valid");
        }

        // GET:  {apiBaseUrl}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {

            // fetch and instantize images using repo
            var images = await imageRepository.GetAllImagesAsync();

            // instantizing an empty response list to hold the response dtos

            var response = new List<ProfileImageDTO>();


            // loop through to assign images into the response
            foreach (var image in images)
            {
                response.Add(new ProfileImageDTO
                {
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Title = image.Title,
                    FileUrl = image.FileUrl,
                    DateCreated = DateTime.Now,
                });
            }
            return Ok(response);
        }
    }
}



