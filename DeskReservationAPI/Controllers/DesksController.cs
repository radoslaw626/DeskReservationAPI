using System.Collections.Generic;
using System.Net.Http;
using DeskReservationAPI.Data;
using DeskReservationAPI.Dto;
using DeskReservationAPI.Dto.DeskDtos;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using DeskReservationAPI.Validators;
using DeskReservationAPI.Validators.DeskDtoValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DesksController(IDeskService deskService)
        {
            _deskService = deskService;
        }
        

        [Authorize(Policy = "TokenPolicy")]
        [Route("api/desk/all")]
        [HttpGet]
        public IActionResult GetAllDesks()
        {
            var desks = _deskService.GetAll();
            var result = new List<DeskGetAllDto>();
            foreach (var desk in desks)
            {
                result.Add(new DeskGetAllDto()
                {
                    Available = desk.Available,
                    City = desk.Location.City,
                    Id = desk.Id,
                    Number = desk.Number
                });
            }

            return Ok(result);

        }
        [Authorize(Policy = "TokenPolicy")]
        [Route("api/desk/all/byLocation")]
        [HttpGet]
        public IActionResult GetAllDesksByLocation([FromBody] DeskCityFilterDto dto)
        {
            var desks = _deskService.GetAllByCity(dto.City);
            var result = new List<DeskGetAllDto>();

            DeskCityFilterDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);


            foreach (var desk in desks)
            {
                result.Add(new DeskGetAllDto()
                {
                    Available = desk.Available,
                    City = desk.Location.City,
                    Id = desk.Id,
                    Number = desk.Number
                });
            }

            return Ok(result);

        }

        [Authorize(Policy = "TokenPolicy", Roles="Admin")]
        [Route("api/desk/availability/change")]
        [HttpPost]
        public IActionResult ChangeDeskAvailability([FromBody] DeskAvailabilityChangeDto dto)
        {
            DeskAvailabilityChangeDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var desk = _deskService.GetDeskById(dto.Id);
            if (desk == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This desk does not exists" });

            _deskService.ChangeDeskAvailability(desk, dto.Available);
            return Ok(new ResponseDto { Status = "Success", Message = "Desk availability changed successfully" });
        }
    }
}
