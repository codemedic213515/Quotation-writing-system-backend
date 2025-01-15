using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data; // Adjust to your project namespace
using QuotationWritingSystem.Models;
using QuotationWritingSystem.DTOs;


namespace QuotationWritingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return BadRequest("Email already exists.");

            // Check if code already exists
            if (await _context.Users.AnyAsync(u => u.Code == registerDto.Code))
                return BadRequest("Code already exists.");

            // Create and save the new user
            var user = new User
            {
                Code = registerDto.Code,
                Name = registerDto.Name,
                Email = registerDto.Email,
                Pwd = BCrypt.Net.BCrypt.HashPassword(registerDto.Pwd),
                Role = registerDto.Role,
                Deleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        
        [HttpGet("test")]
        public IActionResult Test()
{
    return Ok("AuthController is working!");
}
    }
}
