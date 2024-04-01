using UserTodoDotNetWebAPI.Model;

namespace UserTodoDotNetWebAPI.Services.Interface
{
    public interface ITodoRepository : IGenericRepository<Todo>
    {
        public Task<Todo?> Update(Guid Id, Todo todo);
        public Task<IEnumerable<Todo>> GetUserTodos(User user);
        public Task<IEnumerable<Todo>> GetAllTodos();
        public Task<Todo?> GetTodById(Guid Id);
    }
}
