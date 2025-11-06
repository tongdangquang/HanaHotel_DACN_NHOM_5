using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;
using Otel.DtoLayer.DTOs.RoomDTO;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;


        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomService.TGetList();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _roomService.TGetByID(id);
            return Ok(room);
        }

        [HttpPut]
        public IActionResult UpdateRoom(UpdateRoomDTO updateRoomDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var value = _mapper.Map<Room>(updateRoomDTO);

            _roomService.TUpdate(value);
            return Ok("Updated Successfully");
        }

        [HttpPost]
        public IActionResult AddRoom(RoomAddDTO roomAddDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var values = _mapper.Map<Room>(roomAddDTO);
            _roomService.TInsert(values);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _roomService.TGetByID(id);
            if (room == null)
            {
                return NotFound();
            }

            _roomService.TDelete(room);
            return NoContent();
        }
    }
}
