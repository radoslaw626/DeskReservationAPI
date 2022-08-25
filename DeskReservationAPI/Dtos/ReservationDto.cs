using System;

namespace DeskReservationAPI.Dtos
{
    public class ReservationDto
    {
        public ApplicationUserDto User { get; set; }
        public DeskDto Desk { get; set; }
        public DateTime ReservationDateTime { get; set; }
    }
}
