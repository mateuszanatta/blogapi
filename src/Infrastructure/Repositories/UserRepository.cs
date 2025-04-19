using Domain.Models;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogDbContext _context;

    public UserRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<User> RegisterAsync(string email, string password, string name)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public Task<User?> GetAsync(Guid id)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
    }
}

