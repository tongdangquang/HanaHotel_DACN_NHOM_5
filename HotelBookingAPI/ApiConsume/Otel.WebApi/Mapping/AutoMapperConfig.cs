using AutoMapper;
using Otel.DtoLayer.DTOs.RoomDTO;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Mapping
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
