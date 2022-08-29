using DeskReservationAPI.Dto.DeskDtos;
using FluentValidation;

namespace DeskReservationAPI.Validators.DeskDtoValidators
{
    public class DeskAvailabilityChangeDtoValidator : AbstractValidator<DeskAvailabilityChangeDto>
    {
        public DeskAvailabilityChangeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Available)
                .Must(x => x == false || x == true).WithMessage("Available is required");
        }
    }
}