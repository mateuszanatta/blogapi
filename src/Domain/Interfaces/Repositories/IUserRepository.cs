using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> RegisterAsync(string email, string password, string name);
}

