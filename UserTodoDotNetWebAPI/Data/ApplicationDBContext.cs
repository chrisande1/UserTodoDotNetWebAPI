using Microsoft.EntityFrameworkCore;
using UserTodoDotNetWebAPI.Model;

namespace UserTodoDotNetWebAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

    }
}
