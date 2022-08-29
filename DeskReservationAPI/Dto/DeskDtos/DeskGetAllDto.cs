using DeskReservationAPI.Entities;
using Newtonsoft.Json;

namespace DeskReservationAPI.Dto.DeskDtos
{
    public class DeskGetAllDto
    {
        public DeskGetAllDto()
        {
            Available = false;
        }
        public long Id { get; set; }
        //Tag of exact Desk
        public int Number { get; set; }
        public bool? Available { get; set; }
        public string City { get; set; }
    }
}