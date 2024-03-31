using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using UserTodoDotNetWebAPI.DTOs;
using UserTodoDotNetWebAPI.Model;
using UserTodoDotNetWebAPI.Services.Interface;

namespace UserTodoDotNetWebAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(UserRegisterDTO register)
        {
            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = _passwordService.HashPassword(register.Password)
            };

            if (_userRepository.CheckIfEmailExists(user.Email))
            {
                return BadRequest("Email already exists!");
            }

            var result = await _userRepository.Create(user);
            var registeredUser = result.Adapt<UserResponseDTO>();
            var token = _tokenService.CreateUserToken(registeredUser);

            return Ok(new AuthoResponseDTO(user.Id, token));

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDTO request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            var password = request.Password;
            if (user == null)
            {
                return NotFound("Incorrect Email or Password!");
            }

            if (!_passwordService.VerifyPassword(password, user.Password))
            {
                return BadRequest("Incorrect Email or Password!");
            }

            var loginUser = user.Adapt<UserResponseDTO>();
            var token = _tokenService.CreateUserToken(loginUser);

            return Ok(new AuthoResponseDTO(user.Id, token));

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userRepository.GetAll();
            return Ok(result.Adapt<List<UserResponseDTO>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }
            return Ok(targetUser.Adapt<UserResponseDTO>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRegisterDTO request)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }

            targetUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _passwordService.HashPassword(request.Password)
            };

            return Ok(await _userRepository.Update(id, targetUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }

            await _userRepository.DeleteById(id);

            return Ok("User deleted successfully!");
        }

    }
}
