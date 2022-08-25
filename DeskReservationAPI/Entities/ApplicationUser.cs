using Microsoft.AspNetCore.Identity;

namespace DeskReservationAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}