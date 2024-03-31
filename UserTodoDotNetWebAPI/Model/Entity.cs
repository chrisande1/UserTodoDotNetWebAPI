namespace UserTodoDotNetWebAPI.Model
{
    public class Entity
    {
        public Guid Id { get; set; } = new Guid();
        public string? CreatedAt { get; set; } = null;
        public string? UpdatedAt { get; set; } = null;
        public string? DeletedAt { get; set; } = null;
    }
}
