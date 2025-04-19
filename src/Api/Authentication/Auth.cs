using System.Text;
using System.Security.Claims;
using Domain.Models;
using Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Interfaces.Authentication;
using Microsoft.AspNet.Identity;

namespace API.Authentication;

public class Auth : IAuthentication
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public Auth(IConfiguration configuration, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<(User user, string token)> RegisterAsync(string email, string password, string name)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if(user is not null)
        {
            throw new InvalidOperationException("User already exists!");
        }
            
        var newUser = await _userRepository.RegisterAsync(email, _passwordHasher.HashPassword(password), name);


        return (newUser, GenerateJwtToken(newUser));
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user?.Password, password);

        if (user == null || passwordVerificationResult != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
