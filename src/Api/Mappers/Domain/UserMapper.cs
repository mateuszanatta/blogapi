using API.DTOs.Response;
using Domain.Models;

namespace API.Mappers.Domain;

public static class UserMapper
{
    public static RegisterResponse MapToResponse(this User source, string token)
    {
        return new RegisterResponse(source.Id, source.Name, source.Email, token);
    }
}

