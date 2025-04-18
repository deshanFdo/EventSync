using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // POST: api/user/register
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] User user)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Name and password are required.");
        }
        var existing = await _userRepository.GetByNameAsync(user.Name);
        if (existing != null)
        {
            return Conflict("User already exists.");
        }
        await _userRepository.CreateAsync(user);
        return CreatedAtAction(nameof(Register), new { name = user.Name }, user);
    }

    // POST: api/user/login
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByNameAsync(loginRequest.Name);
        if (user == null || user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid name or password.");
        }
        return Ok(user);
    }
}

public class LoginRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}