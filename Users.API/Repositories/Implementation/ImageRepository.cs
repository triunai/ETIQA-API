﻿using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Models.Domain;
using Users.API.Repositories.Interface;

namespace Users.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;


        public ImageRepository(IWebHostEnvironment webHostEnvironment,
                           IHttpContextAccessor httpContextAccessor,
                           ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProfileImage>> GetAllImagesAsync()
        {
            return await dbContext.ProfileImages.ToListAsync();
        }

        public async Task<ProfileImage> Upload(IFormFile file, ProfileImage image)
        {
            // 1 - Upload to API folder --> API/Images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // FileMode.Create specifies that if the file already exists, it will be overwritten; if it doesn't exist, it will be created
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2 - Update the database
            // https://codepulse.com/images/somefilename.jpg
            // scheme://host/basePath/fileName.fileExtension


            // Construct the full URL for the uploaded image.
            // - Scheme: The protocol used (http or https).
            // - Host: The domain name of the server.
            // - Base Path: The root directory of the application on the server.
            // - File Name and Extension: The name and type of the uploaded file.
            var httpRequest = httpContextAccessor.HttpContext.Request; // <-- shortned to make your life easier,  maybe implement this for better chaining
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}{image.FileExtension}"; // check if it has scheme, host, pathBase, fileName and fileExtension

            image.FileUrl = urlPath;

            await dbContext.ProfileImages.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }

   
}
