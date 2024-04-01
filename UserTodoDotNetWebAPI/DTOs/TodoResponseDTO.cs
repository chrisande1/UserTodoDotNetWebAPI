namespace UserTodoDotNetWebAPI.DTOs
{
    public record TodoResponseDTO
    (
        Guid TodoId,
        String Title,
        String Description,
        String User
        );
}
