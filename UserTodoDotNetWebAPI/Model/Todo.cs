namespace UserTodoDotNetWebAPI.Model
{
    public class Todo : Entity
    {
        public required string? Title { get; set; }
        public required string? Description { get; set; }
        public User User { get; set; }
    }
}
