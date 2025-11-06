using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkLocationController : ControllerBase
    {
        private readonly IWorkLocationService _workLocationService;

        public WorkLocationController(IWorkLocationService workLocationService)
        {
            _workLocationService = workLocationService;
        }

        [HttpGet]
        public IActionResult GetWorkLocations()
        {
            var workLocations = _workLocationService.TGetList();
            return Ok(workLocations);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkLocation(int id)
        {
            var workLocation = _workLocationService.TGetByID(id);
            return Ok(workLocation);
        }

        [HttpPut]
        public IActionResult UpdateWorkLocation(WorkLocation workLocation)
        {
            _workLocationService.TUpdate(workLocation);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddworkLocation(WorkLocation workLocation)
        {
            _workLocationService.TInsert(workLocation);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteworkLocation(int id)
        {
            var workLocation = _workLocationService.TGetByID(id);
            if (workLocation == null)
            {
                return NotFound();
            }

            _workLocationService.TDelete(workLocation);
            return NoContent();
        }
    }
}
