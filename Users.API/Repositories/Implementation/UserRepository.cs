using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Models.Domain;
using Users.API.Repositories.Interface;

namespace Users.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        // declare dbContext to perform db operations first and then inject it into ctor
        // ensure everything has async
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            await dbContext.User.AddAsync(user); //method to add into database
            await dbContext.SaveChangesAsync(); //saving changes in databsae
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.User.Include(x => x.Skillset).ToListAsync();

        }

        public async Task<User?> GetUserById(Guid Id)
        {
            return await dbContext.User.Include(x => x.Skillset).FirstOrDefaultAsync(x => x.UserId == Id);
        }

        public async Task<User?> UpdateAsync(User newUser)
        {
            var matchingUserFromDb = await dbContext.User.Include(x => x.Skillset).FirstOrDefaultAsync(data => data.UserId == newUser.UserId);
            if (matchingUserFromDb != null)
            {   

                // basically we take the id we wanna change, and then set its new value using the current values attribute
                dbContext.Entry(matchingUserFromDb).CurrentValues.SetValues(newUser);
                // saving categories next
                matchingUserFromDb.Skillset = matchingUserFromDb.Skillset;

                await dbContext.SaveChangesAsync();
                return matchingUserFromDb;

            }
            return null;
        }
        public async Task<User?> DeleteAsync(Guid Id)
        {
            try
            {
                // will query faster if you remove "Include"
                var existingUser = await dbContext.User.Include(x => x.Skillset).FirstOrDefaultAsync(bp => bp.UserId == Id);

                if (existingUser == null)
                {
                    return null;
                }

                dbContext.User.Remove(existingUser);
                await dbContext.SaveChangesAsync();
                return existingUser;
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }

        }
    }

    }
