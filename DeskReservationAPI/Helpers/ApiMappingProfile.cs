using AutoMapper;
using DeskReservationAPI.Dto;
using DeskReservationAPI.Dto.ReservationDtos;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.Helpers
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<LocationAddDto, Location>();
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<Location, LocationGetAllDto>();
            CreateMap<ReservationDateRangeDto, Reservation>();
        }
    }
}
