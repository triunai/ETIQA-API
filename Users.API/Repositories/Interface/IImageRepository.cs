using Users.API.Models.Domain;

namespace Users.API.Repositories.Interface
{
    public interface IImageRepository

    {
        Task<ProfileImage> Upload(IFormFile file, ProfileImage image);
        Task<IEnumerable<ProfileImage>> GetAllImagesAsync();
    }
}
