using DeskReservationAPI.Dto.DeskDtos;
using DeskReservationAPI.Dto.ReservationDtos;
using FluentValidation;

namespace DeskReservationAPI.Validators.DeskDtoValidators
{
    public class ReservationsByCityFilterDtoValidator : AbstractValidator<ReservationsByCityFilterDto>
    {
        public ReservationsByCityFilterDtoValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");
        }
    }
}