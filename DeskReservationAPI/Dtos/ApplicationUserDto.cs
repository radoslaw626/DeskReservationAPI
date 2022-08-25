using Microsoft.AspNetCore.Identity;

namespace DeskReservationAPI.Dtos
{
    public class ApplicationUserDto : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
