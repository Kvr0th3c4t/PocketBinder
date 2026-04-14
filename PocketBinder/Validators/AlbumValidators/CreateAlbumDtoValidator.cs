using FluentValidation;
using PocketBinder.DTOs.Album;

namespace PocketBinder.Validators.AlbumValidators
{
    public class CreateAlbumDtoValidator : AbstractValidator<CreateAlbumDto>
    {
        public CreateAlbumDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .Must(t => t == "Set" || t == "Custom").WithMessage("Type must be 'Set' or 'Custom'.");
        }
    }
}
