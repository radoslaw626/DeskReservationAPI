using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class LocationDeleteDtoValidator : AbstractValidator<LocationDeleteDto>
    {
        public LocationDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id of Location is required");
        }
    }
}
