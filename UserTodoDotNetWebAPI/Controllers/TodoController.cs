using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserTodoDotNetWebAPI.DTOs;
using UserTodoDotNetWebAPI.Model;
using UserTodoDotNetWebAPI.Services.Interface;

namespace UserTodoDotNetWebAPI.Controllers
{
    [ApiController]
    [Route("api/Todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;
        

       

        public TodoController(ITodoRepository todoRepository, IUserRepository userRepository)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoRequestDTO request)
        {
            var user = await _userRepository.GetById(request.UserId);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description,
                User = user
            };

            var result = await _todoRepository.Create(todo);
            var registeredTodo = result.Adapt<TodoResponseDTO>();
            return Ok(registeredTodo);


        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var result = await _todoRepository.GetAllTodos();
            
            return Ok(result.Adapt<List<TodoResponseDTO>>());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var targetTodo = await _todoRepository.GetTodById(id);

            if (targetTodo == null)
            {
                return NotFound("Todo does not exist!");
                
            }
            var result = targetTodo;
            var todo = result.Adapt<TodoResponseDTO>();
            return Ok(todo);

        }

        [Authorize]
        //Get All Todos By a Single User
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTodosByUser(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            var result = await _todoRepository.GetUserTodos(user);
            var AllTodosByUser = result.Adapt<List<TodoResponseDTO>>();
            return Ok(AllTodosByUser);

        }



        [HttpPut("{id}")]
        public IActionResult UpdateTodo(Guid id, TodoRequestDTO request)
        {
            var targetTodo = _todoRepository.GetById(id);

            if (targetTodo == null)
            {
                return NotFound();
            }

            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description,
            };

            var result = _todoRepository.Update(id, todo);
            var updatedTodo = result.Adapt<TodoResponseDTO>();
            return Ok(updatedTodo);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(Guid id)
        {
            var targetTodo = _todoRepository.GetById(id);

            if (targetTodo == null)
            {
                return NotFound();
            }

            var deletedTodo = _todoRepository.DeleteById(id);
            return NoContent();
        }

    }
}
