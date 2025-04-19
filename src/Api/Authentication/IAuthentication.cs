
using Domain.Models;

namespace Domain.Interfaces.Authentication;

public interface IAuthentication
{
    Task<(User user, string token)> RegisterAsync(string email, string password, string name);
    Task<string> LoginAsync(string email, string password);
}

