using System.Collections.Generic;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.Dto
{
    public class LocationGetAllDto
    {
        public LocationGetAllDto()
        {
            Desks = new List<Desk>();
        }

        public long Id { get; set; }
        public string City { get; set; }
        public virtual IList<Desk> Desks { get; set; }
    }
}