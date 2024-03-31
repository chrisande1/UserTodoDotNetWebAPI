namespace UserTodoDotNetWebAPI.Model
{
    public class User : Entity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }


    }
}
