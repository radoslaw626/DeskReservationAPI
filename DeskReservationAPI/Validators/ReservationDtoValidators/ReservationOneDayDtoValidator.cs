using System;
using System.Security.Cryptography.X509Certificates;
using DeskReservationAPI.Dto.ReservationDtos;
using DeskReservationAPI.Helpers;
using FluentValidation;

namespace DeskReservationAPI.Validators.ReservationDtoValidators
{
    public class ReservationOneDayDtoValidator : AbstractValidator<ReservationOneDayDto>
    {
        public ReservationOneDayDtoValidator()
        {
            RuleFor(x => x.DeskId)
                .NotEmpty().WithMessage("DeskId is required");
            RuleFor(x => x.StartDate)
                .Must(DateParser.BeAValidDate).WithMessage("Invalid date/time")
                .NotEmpty().WithMessage("StartDate is required");


            When(x => DateParser.BeAValidDate(x.StartDate)==true, () =>
            {
                RuleFor(x=>x.StartDate)
                    .Must(x => DateTime.Parse(x) > DateTime.Now).WithMessage("Cannot reserve in past date");
            });
        }

    }
}