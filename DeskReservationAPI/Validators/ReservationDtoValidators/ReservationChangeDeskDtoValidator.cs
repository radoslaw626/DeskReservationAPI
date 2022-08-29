using DeskReservationAPI.Dto.ReservationDtos;
using FluentValidation;

namespace DeskReservationAPI.Validators.ReservationDtoValidators
{
    public class ReservationChangeDeskDtoValidator : AbstractValidator<ReservationChangeDeskDto>
    {
        public ReservationChangeDeskDtoValidator()
        {
            RuleFor(x => x.NewDeskId)
                .NotEmpty().WithMessage("NewDeskId is required");
            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("ReservationId is required");
        }
    }
}