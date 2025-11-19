using Microsoft.AspNetCore.Identity;
using System;

namespace HanaHotel.EntityLayer.Concrete
{
    public class User : IdentityUser<int>
    {
        // Thêm thuộc tính nghiệp vụ của bạn
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }

    public enum GenderType
    {
        Male = 0,
        Female = 1,
    }
}