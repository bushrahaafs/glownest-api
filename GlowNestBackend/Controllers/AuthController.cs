using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GlowNestBackend.Models;
using GlowNestBackend.Data;

namespace GlowNestBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthController(AppDbContext context) => _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("«·»—Ìœ «·≈·ﬂ —Ê‰Ì „” Œœ„ „”»ﬁ«.");

            var user = new User { Name = request.Name, Email = request.Email };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("«·Õ”«» €Ì— „ÊÃÊœ");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("ﬂ·„… «·„—Ê— €Ì— ’ÕÌÕ…");

            return Ok(new { user = new { user.Id, user.Name, user.Email } });
        }
    }
}

