using Microsoft.AspNetCore.Mvc;
using HanaHotel.BusinessLayer.Abstract;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        [HttpGet]
        public IActionResult GetSubscribes()
        {
            var subscribes = _subscribeService.TGetList();
            return Ok(subscribes);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubscribe(int id)
        {
            var subscribe = _subscribeService.TGetByID(id);
            return Ok(subscribe);
        }

        [HttpPut]
        public IActionResult UpdateSubscribe(Subscribe subscribe)
        {
            _subscribeService.TUpdate(subscribe);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddSubscribe(Subscribe subscribe)
        {
            _subscribeService.TInsert(subscribe);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteSubscribe(int id)
        {
            var subscribe = _subscribeService.TGetByID(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            _subscribeService.TDelete(subscribe);
            return NoContent();
        }
    }
}
