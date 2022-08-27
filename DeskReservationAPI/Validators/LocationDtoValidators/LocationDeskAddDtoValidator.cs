using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class LocationDeskAddDtoValidator : AbstractValidator<LocationDeskAddDto>
    {
        public LocationDeskAddDtoValidator()
        {
            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("LocationId required");
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number of desk required");
        }
    }
}
