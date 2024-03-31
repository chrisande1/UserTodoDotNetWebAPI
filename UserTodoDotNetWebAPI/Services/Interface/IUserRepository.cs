using UserTodoDotNetWebAPI.Model;

namespace UserTodoDotNetWebAPI.Services.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> Update(Guid Id, User user);
        public Task<User?> GetUserByEmail(string Email);
        public bool CheckIfEmailExists(string Email);
    }
}
