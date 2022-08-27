using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class AddLocationDtoValidator : AbstractValidator<LocationAddDto>
    {
        public AddLocationDtoValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");
        }
    }
}
