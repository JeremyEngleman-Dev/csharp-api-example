using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace APIExample.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwt;
        private readonly EmployeeDB _db;

        public AuthController(JwtService jwt, EmployeeDB db)
        {
            _jwt = jwt;
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            var token = _jwt.GenerateToken(user);

            return Ok(new { token });
        }
    }

    public record LoginRequest(string Email, string Password);
}