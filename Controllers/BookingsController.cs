using Microsoft.AspNetCore.Mvc;

namespace TESTING_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private static readonly List<Booking> Bookings = new List<Booking>
    {
        new Booking { Id = 123, CustomerName = "John Doe", BookingDate = DateTime.Now, Details = "Booking Details for John Doe" },
        new Booking { Id = 124, CustomerName = "Jane Doe", BookingDate = DateTime.Now.AddDays(1), Details = "Booking Details for Jane Doe" }
    };
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
    }
    public class Booking
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime BookingDate { get; set; }
    public string Details { get; set; }
}
}
