using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Models.Domain;
using Users.API.Repositories.Interface;

namespace Users.API.Repositories.Implementation
{
    public class SkillsetRepository : ISkillsetRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SkillsetRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Skillset?> CreateSkillsetAsync(Skillset skillset)
        {
            await dbContext.Skillset.AddAsync(skillset); //method to add into database
            await dbContext.SaveChangesAsync(); //saving changes in databsae
            return skillset;
        }

        public async Task<IEnumerable<Skillset>> GetAllSkillsetAsync()
        {
            return await dbContext.Skillset.ToListAsync();
        }
        public async Task<Skillset?> GetSkillsetById(Guid Id)
        {
            return await dbContext.Skillset.FirstOrDefaultAsync(x => x.SkillsetId == Id);
        }
        public async Task<Skillset?> DeleteAsync(Guid Id)
        {
            var existingSkillset = await dbContext.Skillset.FirstOrDefaultAsync(x => x.SkillsetId == Id);

            if (existingSkillset != null)
            {
                dbContext.Skillset.Remove(existingSkillset); // written out here
                await dbContext.SaveChangesAsync(); // changes saved here
                return existingSkillset;
            }
            return null;
        }




        public async Task<Skillset?> UpdateAsync(Skillset newSkillset)
        {
            var matchingSkillset = await dbContext.Skillset.FirstOrDefaultAsync(x => x.SkillsetId == newSkillset.SkillsetId);

            if (matchingSkillset != null)
            {
                // ALTERNATE IMPLEMENTATION
                //matchingCategory.Name = category.Name;
                //matchingCategory.UrlHandle = category.UrlHandle

                dbContext.Entry(matchingSkillset).CurrentValues.SetValues(matchingSkillset);
                await dbContext.SaveChangesAsync();
                return matchingSkillset;
            }
            return null;
        }
    }
}
