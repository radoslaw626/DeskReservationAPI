using System;

namespace DeskReservationAPI.Dto.ReservationDtos
{
    public class ReservationOneDayDto
    {
        public long DeskId { get; set; }
        public string StartDate { get; set; } 
    }
}