using FluentValidation;
using PocketBinder.DTOs.Query;

namespace PocketBinder.Validators
{
    public class CardQueryDtoValidator : AbstractValidator<CardQueryDto>
    {
        public CardQueryDtoValidator() 
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(250).WithMessage("PageSize cannot exceed 250.");
        }
    }
}
