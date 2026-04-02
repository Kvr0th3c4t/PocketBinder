namespace PocketBinder.Dtos;

public record RegisterDto(
    string Email,
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string ConfirmPassword
    );