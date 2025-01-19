using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace QuotationWritingSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password) || string.IsNullOrWhiteSpace(userDto.Code) || string.IsNullOrWhiteSpace(userDto.Name))
            {
                return BadRequest("Email, Password, Code, and Name are required.");
            }

            // Check if the email already exists in the database
            var existingEmailUser = _context.Users.SingleOrDefault(u => u.Email == userDto.Email);
            if (existingEmailUser != null)
            {
                return Conflict("Email already exists.");
            }

            // Check if the code already exists in the database
            var existingCodeUser = _context.Users.SingleOrDefault(u => u.Code == userDto.Code);
            if (existingCodeUser != null)
            {
                return Conflict("Code already exists.");
            }

            // Hash password before saving
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new Users  // Change User to Users here
            {
                Email = userDto.Email,
                Password = hashedPassword,  // Save the hashed password
                Code = userDto.Code,
                Name = userDto.Name,
                Role = "User",  // Default role, modify this as per your needs
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for the user by Name (Assuming Name is unique)
            var user = _context.Users.SingleOrDefault(u => u.Name == request.Name);

            // Validate user credentials
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Users user)  // Change User to Users here
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];

            if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtIssuer))
            {
                throw new ArgumentNullException("Jwt:Key or Jwt:Issuer is missing in appsettings.json.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims that represent user details
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Role),
                new Claim(JwtRegisteredClaimNames.NameId, user.Code),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),  // Token expiration
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // LoginRequest DTO for login validation
    public class LoginRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }

    // UserDto for registration validation
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
    }
}
