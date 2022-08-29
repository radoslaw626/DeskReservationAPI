using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DeskReservationAPI.Data;
using DeskReservationAPI.Dto;
using DeskReservationAPI.Dto.ReservationDtos;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using DeskReservationAPI.Validators;
using DeskReservationAPI.Validators.DeskDtoValidators;
using DeskReservationAPI.Validators.ReservationDtoValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IDeskService _deskService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReservationsController(IReservationService reservationService, IDeskService deskService,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _reservationService = reservationService;
            _deskService = deskService;
            _userManager = userManager;
            _context = context;
        }


        [Authorize(Policy = "TokenPolicy")]
        [Route("api/reservation/oneDay/add")]
        [HttpPost]
        public IActionResult ReserveForOneDay([FromBody] ReservationOneDayDto dto)
        {
            ReservationOneDayDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var desk = _deskService.GetDeskById(dto.DeskId);
            if (desk == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk does not exists" });

            if(!_reservationService.CheckIfDeskIsAvailable(desk))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk is not available" });

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userNameClaim = claimsIdentity.Name;
                if (userNameClaim != null)
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == userNameClaim);
                    var reservation = new Reservation()
                    {
                        Desk = desk,
                        StartDate = DateTime.Parse(dto.StartDate),
                        EndDate = DateTime.Parse(dto.StartDate).AddDays(1),
                        User = user
                    };
                    var leftDays = _reservationService.CheckCurrentReservationsLeftTime(user);
                    if (1 > leftDays)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ResponseDto { Status = "Error", Message = "You cannot have more than 7 days of reservation." });
                    }
                    if (_reservationService.CheckIfDeskIsReserved(desk, reservation.StartDate, reservation.EndDate))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ResponseDto { Status = "Error", Message = "Desk is already reserved in that date." });
                    }
                    _reservationService.AddReservation(reservation);
                }
            }

            return Ok(new ResponseDto { Status = "Success", Message = "Reservation added successfully" });
        }
        [Authorize(Policy = "TokenPolicy")]
        [Route("api/reservation/dateRange/add")]
        [HttpPost]
        public IActionResult ReserveForDateRange([FromBody] ReservationDateRangeDto dto)
        {
            ReservationDateRangeDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var desk = _deskService.GetDeskById(dto.DeskId);
            if (desk == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk does not exists" });
            if (!_reservationService.CheckIfDeskIsAvailable(desk))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk is not available" });
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userNameClaim = claimsIdentity.Name;
                if (userNameClaim != null)
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == userNameClaim);
                    var reservation = new Reservation()
                    {
                        Desk = desk,
                        StartDate = DateTime.Parse(dto.StartDate),
                        EndDate = DateTime.Parse(dto.EndDate).AddDays(1),
                        User = user
                    };
                    var leftDays = _reservationService.CheckCurrentReservationsLeftTime(user);
                    if ((reservation.EndDate - reservation.StartDate).TotalDays > leftDays)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ResponseDto { Status = "Error", Message = "You cannot have more than 7 days of reservation." });
                    }

                    if (_reservationService.CheckIfDeskIsReserved(desk, reservation.StartDate, reservation.EndDate))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ResponseDto { Status = "Error", Message = "Desk is already reserved in that date." });
                    }
                    _reservationService.AddReservation(reservation);
                }
            }

            return Ok(new ResponseDto { Status = "Success", Message = "Reservation added successfully" });
        }

        [Authorize(Policy = "TokenPolicy")]
        [Route("api/reservation/desk/change")]
        [HttpPost]
        public IActionResult ChangeDesk([FromBody] ReservationChangeDeskDto dto)
        {
            ReservationChangeDeskDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var newDesk = _deskService.GetDeskById(dto.NewDeskId);
            if (newDesk == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk does not exists" });

            if (!_reservationService.CheckIfDeskIsAvailable(newDesk))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Desk is not available" });

            var reservation = _reservationService.GetReservationById(dto.ReservationId);
            if(reservation.StartDate<DateTime.Now.AddHours(24))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "You can only change desks at least 24 hours before reservation" });

            _reservationService.ChangeDesk(reservation, newDesk);

            return Ok(new ResponseDto { Status = "Success", Message = "Desk changed successfully" });
        }

        [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
        [Route("api/reservation/getAll")]
        [HttpGet]
        public IActionResult AllReservationsByCity([FromBody] ReservationsByCityFilterDto dto)
        {
            ReservationsByCityFilterDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);
            var result = new List<ReservationsAllDto>();
            var reservations = _reservationService.GetAllReservations();
            foreach (var reservation in reservations)
            {
                result.Add(new ReservationsAllDto()
                {
                    DeskId = reservation.Desk.Id,
                    Id=reservation.Id,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    FirstName = reservation.User.FirstName,
                    LastName = reservation.User.LastName,
                });
            }

            return Ok(result);
        }

    }
}