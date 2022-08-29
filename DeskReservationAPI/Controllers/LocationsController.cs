using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskReservationAPI.Data;
using DeskReservationAPI.Dto;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using DeskReservationAPI.Validators;
using Microsoft.AspNetCore.Authorization;

namespace DeskReservationAPI.Controllers
{
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IDeskService _deskService;
        private readonly IReservationService _reservationService;

        public LocationsController(ILocationService locationService, IMapper mapper, IDeskService deskService, IReservationService reservationService)
        {
            _locationService = locationService;
            _mapper = mapper;
            _deskService = deskService;
            _reservationService = reservationService;
        }

        [Authorize(Policy = "TokenPolicy")]
        [Route("api/location/all")]
        [HttpGet]
        public IActionResult GetAllLocations()
        {
            return Ok(_mapper.Map<IEnumerable<LocationGetAllDto>>(_locationService.GetAll()));
        }



        [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
        [Route("api/location/add")]
        [HttpPost]
        public IActionResult AddLocation([FromBody] LocationAddDto dto)
        {
            AddLocationDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var location = _locationService.GetLocationByCity(dto.City);
            if (location != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This Location already exists" });

            _locationService.AddLocation(_mapper.Map<Location>(dto));
            return Ok(new ResponseDto { Status = "Success", Message = "Location added successfully" });
        }

        [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
        [Route("api/location/remove")]
        [HttpDelete]
        public IActionResult DeleteLocation([FromBody] LocationDeleteDto dto)
        {
            LocationDeleteDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var location = _locationService.GetLocationById(dto.Id);
            if (location == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This location does not exists" });

            if (location.Desks.Count > 0)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This location contains Desks" });

            _locationService.DeleteLocation(location);
            return Ok(new ResponseDto { Status = "Success", Message = "Location removed successfully" });
        }

        [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
        [Route("api/location/desk/add")]
        [HttpPost]
        public IActionResult LocationDeskAdd([FromBody] LocationDeskAddDto dto)
        {
            LocationDeskAddDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var location = _locationService.GetLocationById(dto.LocationId);
            if (location == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This location does not exists" });

            _locationService.AddDeskToLocation(location, new Desk() { Number = dto.Number });
            return Ok(new ResponseDto { Status = "Success", Message = "Desk added successfully" });
        }
        [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
        [Route("api/location/desk/remove")]
        [HttpDelete]
        public IActionResult LocationDeskRemove([FromBody] LocationDeskRemoveDto dto)
        {
            LocationDeskRemoveDtoValidator validator = new();
            var validResult = validator.Validate(dto);
            if (!validResult.IsValid)
                return BadRequest(validResult.Errors);

            var location = _locationService.GetLocationById(dto.LocationId);
            if (location == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This location does not exists" });

            var desk = _deskService.GetDeskById(dto.DeskId);
            if (desk == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This desk does not exists" });
            if(_reservationService.GetAllReservationsOfDesk(desk)!=null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "This desk have reservations" });

            _locationService.RemoveDeskFromLocation(location, desk);
            return Ok(new ResponseDto { Status = "Success", Message = "Desk removed successfully" });
        }
    }
}
