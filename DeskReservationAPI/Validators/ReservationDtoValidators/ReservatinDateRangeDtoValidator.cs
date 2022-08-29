using System;
using System.Security.Cryptography.X509Certificates;
using DeskReservationAPI.Dto.ReservationDtos;
using DeskReservationAPI.Helpers;
using FluentValidation;

namespace DeskReservationAPI.Validators.ReservationDtoValidators
{
    public class ReservationDateRangeDtoValidator : AbstractValidator<ReservationDateRangeDto>
    {
        public ReservationDateRangeDtoValidator()
        {
            RuleFor(x => x.DeskId)
                .NotEmpty().WithMessage("DeskId is required");
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required")
                .Must(DateParser.BeAValidDate).WithMessage("Invalid date/time");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required")
                .Must(DateParser.BeAValidDate).WithMessage("Invalid date/time");


            When(x => DateParser.BeAValidDate(x.StartDate) == true &&
                      DateParser.BeAValidDate(x.EndDate) == true, () =>
            {
                RuleFor(x => x.StartDate)
                    .Must(x => DateTime.Parse(x) > DateTime.Now).WithMessage("Cannot reserve in past date");
                RuleFor(x => x)
                    .Must(x => DateTime.Parse(x.EndDate) > DateTime.Parse(x.StartDate))
                    .WithMessage("EndTime must be greater than StartDate");
            });


        }

    }
}