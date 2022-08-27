using DeskReservationAPI.Dto;
using FluentValidation;

namespace DeskReservationAPI.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty().WithMessage("Your Email cannot be empty");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Your FirstName cannot be empty");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Your LastName cannot be empty");
            RuleFor(x=>x.UserName)
                .NotEmpty().WithMessage("Your UserName cannot be empty")
                .Matches(@"^[A-Za-z]+$").WithMessage("Special characters in UserName are not allowed");
            RuleFor(x=>x.Password)
                .NotEmpty().WithMessage("Your Password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
