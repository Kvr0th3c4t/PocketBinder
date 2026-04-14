using FluentValidation;
using PocketBinder.DTOs.Collection;

namespace PocketBinder.Validators.CollectionValidators
{
    public class AddCardToCollectionDtoValidator : AbstractValidator<AddCardToCollectionDto>
    {
        public AddCardToCollectionDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(99).WithMessage("Quantity cannot exceed 99.");
        }
    }
}
