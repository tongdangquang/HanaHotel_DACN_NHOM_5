using AutoMapper;
using HanaHotel.DtoLayer.DTOs.RoomDTO;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Room, RoomAddDTO>().ReverseMap();
            CreateMap<Room, UpdateRoomDTO>().ReverseMap();
        }
    }
}
