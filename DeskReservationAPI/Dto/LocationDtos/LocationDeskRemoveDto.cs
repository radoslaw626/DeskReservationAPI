using System.Security.AccessControl;

namespace DeskReservationAPI.Dto
{
    public class LocationDeskRemoveDto
    {
        public long LocationId { get; set; }
        public int DeskId { get; set; }
    }
}
