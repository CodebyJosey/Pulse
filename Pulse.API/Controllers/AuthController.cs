using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pulse.API.Domain.Users;
using Pulse.API.Infrastructure.Persistence;
using Pulse.API.Security;

namespace Pulse.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly PulseDbContext _db;
    private readonly JwtTokenService _jwt;

    public AuthController(PulseDbContext db, JwtTokenService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            return BadRequest("Email already exists");

        User? user = new User
        {
            Email = email,
            PasswordHash = PasswordHasher.Hash(password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(string email, string password)
    {
        User? user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            return Unauthorized();
        }

        return Ok(_jwt.CreateToken(user));
    }
}
