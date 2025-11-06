using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;
using Otel.EntityLayer.Concrete;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {

        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public IActionResult GetStaffs()
        {
            var stuffs = _staffService.TGetList();
            return Ok(stuffs);
        }

        [HttpGet("{id}")]
        public IActionResult GetStaff(int id)
        {
            var value = _staffService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateStaff(Staff staff)
        {
            _staffService.TUpdate(staff);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddStaff(Staff staff)
        {
            _staffService.TInsert(staff);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            var staff = _staffService.TGetByID(id);
            if (staff == null)
            {
                return NotFound();
            }

            _staffService.TDelete(staff);
            return NoContent();
        }
    }
}
