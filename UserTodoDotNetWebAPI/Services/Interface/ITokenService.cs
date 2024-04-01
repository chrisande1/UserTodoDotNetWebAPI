using UserTodoDotNetWebAPI.DTOs;

namespace UserTodoDotNetWebAPI.Services.Interface
{
    public interface ITokenService
    {
        public string CreateUserToken(UserResponseDTO user);
        
    }
}
