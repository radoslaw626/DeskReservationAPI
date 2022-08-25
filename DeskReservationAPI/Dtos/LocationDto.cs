using System.Collections.Generic;

namespace DeskReservationAPI.Dtos
{
    public class LocationDto
    {
        public LocationDto()
        {
            Desks = new List<DeskDto>();
        }
        public string City { get; set; }
        public virtual IList<DeskDto> Desks { get; set; }
    }
}
