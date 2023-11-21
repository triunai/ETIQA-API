using Users.API.Models.Domain;

namespace Users.API.Repositories.Interface
{
    public interface ISkillsetRepository
    {
        Task<Skillset?> CreateSkillsetAsync(Skillset skillset);
        //using IEnumerable here to ensure data is read only, suited for get methods, and also has Deferred Execution
        Task<IEnumerable<Skillset>> GetAllSkillsetAsync();

        // no need to use IEnumerable because we are dealing with a single response
        // will return a skillset or return null
        Task<Skillset?> GetSkillsetById(Guid Id);

        // will return an updated object or null
        Task<Skillset?> UpdateAsync(Skillset newSkillset);

        Task<Skillset?> DeleteAsync(Guid Id);
    }
}
