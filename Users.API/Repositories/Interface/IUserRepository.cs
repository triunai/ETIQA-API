using Users.API.Models.Domain;

namespace Users.API.Repositories.Interface
{
    public interface IUserRepository
    {
        // we need 5 methods: create,get, getAll, update, delete

        Task<User?> CreateUserAsync(User user);
        //using IEnumerable here to ensure data is read only, suited for get methods, and also has Deferred Execution
        Task<IEnumerable<User>> GetAllUsersAsync();

        // no need to use IEnumerable because we are dealing with a single response
        // will return a user or return null
        Task<User?> GetUserById(Guid Id);

        // will return an updated object or null
        Task<User?> UpdateAsync(User newUser);

        Task<User?> DeleteAsync(Guid Id);
    }
}
