using System.Collections;
using System.Collections.Generic;

namespace DeskReservationAPI.Entities
{
    public class Location
    {
        public Location()
        {
            Desks = new List<Desk>();
        }

        public long Id { get; set; }
        public string City { get; set; }
        public virtual IList<Desk> Desks { get; set; }
    }
}
