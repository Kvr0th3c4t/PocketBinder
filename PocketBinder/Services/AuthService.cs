using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PocketBinder.Data;
using PocketBinder.Dtos;
using PocketBinder.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PocketBinder.Services;

public class AuthService : IAuthService
{
    // Inyectamos el ApplicationDbContext y IConfiguration para acceder a la base de datos y a la configuración de JWT
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // Método para manejar el login de usuarios
    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null; // Invalid email or password
        }

        var token = GenerateJwtToken(user);
        return new LoginResponseDto(token, user.Email);
    }

    // Método para manejar el registro de nuevos usuarios
    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        if(await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return false; // User with the same email already exists
        }

        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return false; // Password and confirm password do not match
        }

        var user = new User
        {
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.UserName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Hash the password
            CreatedAt = DateTime.UtcNow
        };
     
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return true;
    }

    //  Método privado para generar un token JWT para el usuario autenticado
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new System.Security.Claims.Claim("UserId", user.UserId.ToString()),
            new System.Security.Claims.Claim("Email", user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}