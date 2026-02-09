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

        public AuthController(JwtService jwt)
        {
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // TODO: validate user (DB, Identity, etc.)
            if (request.Email != "test@test.com" || request.Password != "password")
                return Unauthorized();

            var token = _jwt.GenerateToken("1", request.Email);

            return Ok(new { token });
        }
    }

    public record LoginRequest(string Email, string Password);
}