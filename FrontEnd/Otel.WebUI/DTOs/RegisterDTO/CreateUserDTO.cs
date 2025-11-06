using System.ComponentModel.DataAnnotations;

namespace Otel.WebUI.DTOs.RegisterDTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Work Location is required.")]
        public int WorkLocationId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
