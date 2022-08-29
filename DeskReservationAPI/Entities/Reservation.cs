using System;

namespace DeskReservationAPI.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public ApplicationUser User { get; set; }
        public Desk Desk { get; set; }
        public DateTime StartDate { get; set; }   
        public DateTime EndDate { get; set; }   

    }
}
