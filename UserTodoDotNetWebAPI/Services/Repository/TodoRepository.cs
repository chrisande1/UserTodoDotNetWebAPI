using Microsoft.EntityFrameworkCore;
using UserTodoDotNetWebAPI.Data;
using UserTodoDotNetWebAPI.Model;
using UserTodoDotNetWebAPI.Services.Interface;

namespace UserTodoDotNetWebAPI.Services.Repository
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDBContext dBContext) : base(dBContext)
        {

        }

        public async Task<Todo?> Update(Guid Id, Todo todo)
        {
            var targetTodo = await GetById(Id);

            targetTodo.Title = todo.Title;
            targetTodo.Description = todo.Description;


            targetTodo.UpdatedAt = DateTime.Now.ToString();
            await _dBContext.SaveChangesAsync();
            return targetTodo;
        }

        public async Task <IEnumerable<Todo>> GetUserTodos(User user)
        {

            var todos = await _dBContext.Todos
                .Include(todo => todo.User)
                .Where(todo => todo.User == user).ToListAsync();
            return  todos;
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _dBContext.Todos
             .Include(todo => todo.User)
             .ToListAsync();
        }

        public async Task<Todo?> GetTodById(Guid Id)
        {
            return await _dBContext.Todos
                .Include(todo => todo.User)
                .FirstOrDefaultAsync(todo => todo.Id == Id);
        }
    }
}
