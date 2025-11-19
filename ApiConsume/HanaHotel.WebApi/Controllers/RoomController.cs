using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HanaHotel.BusinessLayer.Abstract;
using HanaHotel.DataAccessLayer.Abstract;
using HanaHotel.DtoLayer.DTOs.RoomDTO;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IImageDal _imageDal;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IImageDal imageDal, IMapper mapper)
        {
            _roomService = roomService;
            _imageDal = imageDal;
            _mapper = mapper;
        }

        // GET: api/room
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomService.TGetList();
            var images = _imageDal.GetList();

            var result = rooms.Select(r =>
            {
                var dto = _mapper.Map<ResultRoomDTO>(r);
                dto.ImagePaths = images
                    .Where(i => i.RoomId == r.Id)
                    .Select(i => i.ImagePath)
                    .ToList();
                return dto;
            }).ToList();

            return Ok(result);
        }

        // GET: api/room/5
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _roomService.TGetByID(id);
            if (room == null)
                return NotFound();

            var dto = _mapper.Map<ResultRoomDTO>(room);
            dto.ImagePaths = _imageDal.GetList()
                .Where(i => i.RoomId == room.Id)
                .Select(i => i.ImagePath)
                .ToList();

            return Ok(dto);
        }

        // PUT: api/room
        [HttpPut]
        public IActionResult UpdateRoom(UpdateRoomDTO updateRoomDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var value = _mapper.Map<Room>(updateRoomDTO);
            _roomService.TUpdate(value);
            return Ok("Updated Successfully");
        }

        // POST: api/room
        [HttpPost]
        public IActionResult AddRoom(RoomAddDTO roomAddDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var values = _mapper.Map<Room>(roomAddDTO);
            _roomService.TInsert(values);

            return Ok();
        }

        // DELETE: api/room/5
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
