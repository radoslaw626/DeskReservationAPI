using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class LocationDeskRemoveDtoValidator : AbstractValidator<LocationDeskRemoveDto>
    {
        public LocationDeskRemoveDtoValidator()
        {
            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("LocationId required");
            RuleFor(x => x.DeskId)
                .NotEmpty().WithMessage("DeskId required");
        }
    }
}
