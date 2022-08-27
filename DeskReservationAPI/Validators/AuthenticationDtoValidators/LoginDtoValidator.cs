using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Your UserName cannot be empty");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Your Password cannot be empty");
        }
    }
}
