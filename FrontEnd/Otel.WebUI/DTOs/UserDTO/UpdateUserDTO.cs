using System.ComponentModel.DataAnnotations;

namespace Otel.WebUI.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Work Location is required.")]
        [Display(Name = "Work Location")]
        public int WorkLocationId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$",
            ErrorMessage = "Password must be at least 5 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
