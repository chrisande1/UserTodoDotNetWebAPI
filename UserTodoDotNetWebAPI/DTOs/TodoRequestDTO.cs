using UserTodoDotNetWebAPI.Model;

namespace UserTodoDotNetWebAPI.DTOs
{
    public record TodoRequestDTO
    (
        string Title,
        string Description,
        Guid UserId
        );
}
