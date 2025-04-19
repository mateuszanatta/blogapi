namespace API.DTOs.Response
{
    public record RegisterResponse(Guid Id, string Name, string Email, string token);
}
