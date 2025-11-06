using AutoMapper;
using Otel.EntityLayer.Concrete;
using Otel.WebUI.DTOs.AboutDTO;
using Otel.WebUI.DTOs.BookingDTO;
using Otel.WebUI.DTOs.ContactDTO;
using Otel.WebUI.DTOs.GuestDTO;
using Otel.WebUI.DTOs.LoginDTO;
using Otel.WebUI.DTOs.MessageCategoryDTO;
using Otel.WebUI.DTOs.RegisterDTO;
using Otel.WebUI.DTOs.RoleDTO;
using Otel.WebUI.DTOs.RoomDTO;
using Otel.WebUI.DTOs.ServiceDTO;
using Otel.WebUI.DTOs.StaffDTO;
using Otel.WebUI.DTOs.TestimonialDTO;
using Otel.WebUI.DTOs.UserDTO;
using Otel.WebUI.DTOs.WorkLocationDTO;

namespace Otel.WebUI.Mapping
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
