using System.ComponentModel.DataAnnotations;

namespace HanaHotel.WebUI.DTOs.UserDTO
{
    public class EditAccountDTO
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Không được để trống họ")]
		[Display(Name = "Họ tên")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Không được để trống tên")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Không được để trống Username")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Không được để trống email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Thành phố")]
        public string? City { get; set; }

        [Display(Name = "Công việc")]
        public string? Department { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{5,}$",
            ErrorMessage = "Mật khẩu phải có ít nhất 5 kí tự, bao gồm ít nhất một kí tự viết hoa, một kí tự thường, một số và một kí tự đặc biệt")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string? ConfirmPassword { get; set; }
    }
}