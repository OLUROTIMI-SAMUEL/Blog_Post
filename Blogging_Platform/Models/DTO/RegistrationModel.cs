using System.ComponentModel.DataAnnotations;

namespace Blogging_Platform.Models.DTO
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$",
            ErrorMessage = "Please Password should contain at least Minimum of 6 character,1 uppercase, 1Lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }  //PLEASE NOTE TO GET THE REGEX FOR YOUR PASSWORD YOU CAN GO TO "IHATEREGEX .com"
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string? Role { get; set; }
    }
}
