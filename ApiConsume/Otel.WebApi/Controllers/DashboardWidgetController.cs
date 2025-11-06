using Microsoft.AspNetCore.Mvc;
using Otel.BusinessLayer.Abstract;

namespace Otel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardWidgetController : ControllerBase
    {
        private readonly IGuestService _guestService;
        private readonly IStaffService _staffService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public DashboardWidgetController(IGuestService guestService, IStaffService staffService, IRoomService roomService, IBookingService bookingService)
        {
            _guestService = guestService;
            _staffService = staffService;
            _roomService = roomService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetDashboardWidgetCounts()
        {

            var guest = _guestService.TGetList();
            var staffs = _staffService.TGetList();
            var rooms = _roomService.TGetList();
            var bookings = _bookingService.TGetList();

            var widgetCounts = new Dictionary<string, string>
            {
                { "Guests", guest.Count.ToString() },
                { "Staffs", staffs.Count.ToString() },
                { "Rooms", rooms.Count.ToString() },
                { "Bookings", bookings.Count.ToString() }
            };

            return Ok(widgetCounts);
        }
    }
}
