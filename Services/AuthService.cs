using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppointmentApi.Data;
using TruckingCompanyApi.Models;
using Microsoft.AspNetCore.Mvc;
using AuthlApi.Models;



public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly string _secretKey; // Store secret key securely

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _secretKey = configuration["Jwt:Key"]; // Get secret key from configuration
    }
    public async Task<bool> Register(AuthModel model)
    {
        if (_context.Users.Any(u => u.Username == model.Username))
        {
            return false; // User already exists
        }

        // Create a new user and store it in the database
        var user = new User
        {
            Username = model.Username,
            Password = HashPassword(model.Password),
            Role = model.Role // Store role in the User entity
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> Login(AuthModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            return null; // Invalid credentials
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role) // Assign role from user data
        };

        return GenerateToken(claims);
    }

    public async Task<List<string>> GetUserRoles(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user != null ? new List<string> { user.Role } : new List<string>(); // Return roles as a list
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5000",
            audience: "http://localhost:5000",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        // Implement password hashing
        return password; // Replace with actual hashing logic
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // Implement password verification
        return inputPassword == storedPassword; // Replace with actual verification logic
    }

  
}


