using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
