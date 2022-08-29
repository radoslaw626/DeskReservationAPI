using DeskReservationAPI.Dto.DeskDtos;
using FluentValidation;

namespace DeskReservationAPI.Validators.DeskDtoValidators
{
    public class DeskCityFilterDtoValidator : AbstractValidator<DeskCityFilterDto>
    {
        public DeskCityFilterDtoValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");
        }
    }
}