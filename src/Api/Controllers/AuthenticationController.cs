using Microsoft.AspNetCore.Mvc;
using API.DTOs.Requests;
using API.DTOs.Response;
using API.Mappers.Domain;
using Domain.Interfaces.Authentication;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthentication _authentication;

    public AuthenticationController(IAuthentication authentication)
    {
        _authentication = authentication;
    }

    [HttpPost("register")]
    [EndpointSummary("Register a new user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest userRequest)
    {
        try
        {
            var (user, token) = await _authentication.RegisterAsync(userRequest.Email, userRequest.Password, userRequest.Name);

            var response = user.MapToResponse(token);

            return Ok(response);
        }
        catch (InvalidOperationException ex) 
        { 
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("login")]
    [EndpointSummary("Login user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> LoginAsync([FromBody] LoginRequest userRequest)
    {
        try
        {
            var token = await _authentication.LoginAsync(userRequest.Email, userRequest.Password);

            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}

