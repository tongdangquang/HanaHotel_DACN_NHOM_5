using AutoMapper;
using HanaHotel.EntityLayer.Concrete;
using HanaHotel.WebUI.DTOs.AboutDTO;
using HanaHotel.WebUI.DTOs.BookingDTO;
using HanaHotel.WebUI.DTOs.ContactDTO;
using HanaHotel.WebUI.DTOs.GuestDTO;
using HanaHotel.WebUI.DTOs.LoginDTO;
using HanaHotel.WebUI.DTOs.MessageCategoryDTO;
using HanaHotel.WebUI.DTOs.RegisterDTO;
using HanaHotel.WebUI.DTOs.RoleDTO;
using HanaHotel.WebUI.DTOs.RoomDTO;
using HanaHotel.WebUI.DTOs.ServiceDTO;
using HanaHotel.WebUI.DTOs.StaffDTO;
using HanaHotel.WebUI.DTOs.TestimonialDTO;
using HanaHotel.WebUI.DTOs.UserDTO;
using HanaHotel.WebUI.DTOs.WorkLocationDTO;

namespace HanaHotel.WebUI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Service DTO'ları
            CreateMap<ResultServiceDTO, Service>().ReverseMap();
            CreateMap<CreateServiceDTO, Service>().ReverseMap();
            CreateMap<UpdateServiceDTO, Service>().ReverseMap();

            // Room DTO'ları
            CreateMap<ResultRoomDTO, Room>().ReverseMap();
            CreateMap<AddRoomDTO, Room>().ReverseMap();
            CreateMap<UpdateRoomDTO, Room>().ReverseMap();

            // Guest DTO'ları
            CreateMap<ResultGuestDTO, Guest>().ReverseMap();
            CreateMap<CreateGuestDTO, Guest>().ReverseMap();
            CreateMap<UpdateGuestDTO, Guest>().ReverseMap();

            // User DTO'ları
            CreateMap<CreateUserDTO, AppUser>().ReverseMap();
            CreateMap<LoginUserDTO, AppUser>().ReverseMap();
            CreateMap<UpdateUserDTO, AppUser>().ReverseMap();


            // About DTO'ları
            CreateMap<ResultAboutDTO, About>().ReverseMap();

            // Testimonial DTO'ları
            CreateMap<ResultTestimonialDTO, Testimonial>().ReverseMap();
            CreateMap<AddTestimonialDTO, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDTO, Testimonial>().ReverseMap();

            // Staff DTO'ları
            CreateMap<ResultStaffDTO, Staff>().ReverseMap();

            // Booking DTO'ları
            CreateMap<CreateBookingDTO, Booking>().ReverseMap();
            CreateMap<UpdateBookingDTO, Booking>().ReverseMap();

            // Contact DTO'ları
            CreateMap<CreateContactDTO, Contact>().ReverseMap();
            CreateMap<DetailContactDTO, Contact>().ReverseMap();
            CreateMap<InboxContactDTO, Contact>().ReverseMap();
            CreateMap<SendMessageDTO, Contact>().ReverseMap();
            CreateMap<AdminDashboardContactDTO, Contact>().ReverseMap();

            //Work Location DTO'Ları 
            CreateMap<ResultWorkLocationDTO, WorkLocation>().ReverseMap();
            CreateMap<CreateWorkLocationDTO, WorkLocation>().ReverseMap();
            CreateMap<UpdateWorkLocationDTO, WorkLocation>().ReverseMap();

            //Rol DTO'Ları
            CreateMap<CreateRoleDTO, AppRole>().ReverseMap();
            CreateMap<UpdateRoleDTO, AppRole>().ReverseMap();

            //Message Category DTO'Ları
            CreateMap<ResultMessageCategoryDTO, MessageCategory>().ReverseMap();
            CreateMap<CreateMessageCategoryDTO, MessageCategory>().ReverseMap();
            CreateMap<UpdateMessageCategoryDTO, MessageCategory>().ReverseMap();
        }
    }
}
