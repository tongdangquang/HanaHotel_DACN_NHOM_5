using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            var services = _serviceService.TGetList();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var service = _serviceService.TGetByID(id);
            return Ok(service);
        }

        [HttpPut]
        public IActionResult UpdateService(Service service)
        {
            _serviceService.TUpdate(service);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddService(Service service)
        {
            _serviceService.TInsert(service);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = _serviceService.TGetByID(id);
            if (service == null)
            {
                return NotFound();
            }

            _serviceService.TDelete(service);
            return NoContent();
        }
    }
}
