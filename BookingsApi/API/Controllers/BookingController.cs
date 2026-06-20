using Microsoft.AspNetCore.Mvc;
using BookingsApi.Models;
using BookingsApi.Services;

namespace BookingsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController(IBookingService _service) : BaseController
    {
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAll()
        {
            var bookings = _service.GetAll();
            return ToActionResult(bookings);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Booking> GetById(int id)
        {
            var booking = _service.GetById(id);
            return ToActionResult(booking);
        }

        [HttpPost]
        public ActionResult<Booking> Create([FromBody] CreateBookingRequest booking)
        {
            var created = _service.Create(booking);
            return ToCreatedResult(created, nameof(GetById), b => new { id = b.Id });
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Cancel(id);
            return ToNoContentResult(result);
        }
    }
}
