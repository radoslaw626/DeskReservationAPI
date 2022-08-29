using System;

namespace DeskReservationAPI.Dto.ReservationDtos
{
    public class ReservationsAllDto
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long DeskId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
