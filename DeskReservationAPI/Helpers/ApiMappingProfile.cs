using AutoMapper;
using DeskReservationAPI.Dtos;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.Helpers
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<Desk, DeskDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
