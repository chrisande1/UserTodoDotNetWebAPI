using Microsoft.EntityFrameworkCore;
using UserTodoDotNetWebAPI.Data;
using UserTodoDotNetWebAPI.Model;
using UserTodoDotNetWebAPI.Services.Interface;

namespace UserTodoDotNetWebAPI.Services.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            
        }

        public async Task<User> Update(Guid Id, User user)
        {
            var targetUser = await GetById(Id);

            targetUser.Name = user.Name;
            targetUser.Email = user.Email;
            targetUser.Password = user.Password;

            targetUser.UpdatedAt = DateTime.Now.ToString();
            await _dBContext.SaveChangesAsync();
            return targetUser;
        }
        public bool CheckIfEmailExists(string Email)
        {
            return _dBContext.Users.Any(user => user.Email == Email);
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(user => user.Email == Email);
        }
    }
}
