using PocketBinder.Dtos;

namespace PocketBinder.Services;

public interface IAuthService
{
    // Método para manejar el login de usuarios, devuelve un LoginResponseDto con el token JWT y el email del usuario
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);

    // Método para manejar el registro de nuevos usuarios, devuelve un booleano indicando si el registro fue exitoso o no
    Task<bool> RegisterAsync(RegisterDto registerDto);
}