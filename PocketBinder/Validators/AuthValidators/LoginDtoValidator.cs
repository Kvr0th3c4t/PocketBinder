using FluentValidation;
using PocketBinder.DTOs.Auth;

namespace PocketBinder.Validators.AuthValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(9).WithMessage("Password must be at least 9 characters long.");
        }
    }
}
