using System.ComponentModel.DataAnnotations;

namespace DeskReservationAPI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        [RegularExpression(@"^[A-Za-z ]+$",
            ErrorMessage = "Special Characters are not allowed.")]
        public string UserName { get; set; }
        [EmailAddress]
        
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
