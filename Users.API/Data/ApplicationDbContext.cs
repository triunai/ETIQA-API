using Microsoft.EntityFrameworkCore;
using Users.API.Models.Domain;
namespace Users.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Skillset> Skillset { get; set; }

        public DbSet<ProfileImage> ProfileImages { get; set; }
        // add skillset here if you need it
    }
}
