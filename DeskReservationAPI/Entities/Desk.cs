using Newtonsoft.Json;

namespace DeskReservationAPI.Entities
{
    public class Desk
    {
        public Desk()
        {
            Available = false;
        }
        public long Id { get; set; }
        //Tag of exact Desk
        public int Number { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public Location Location;
    }
}
