using BookChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingRepository _bookingRepository;

        public BookingController(ILogger<BookingController> logger, IBookingRepository bookingRepository)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var booking = await _bookingRepository.Get(id);
                if (booking == null)
                    return NotFound();

                //TODO: Use automapper to DTO
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("BookingController.Get", id);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public IActionResult CheckAvailebleDates(DateTime from, DateTime to)
        {
            try
            {
                return Ok(_bookingRepository.CheckRoomAvaiability(from, to));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("BookingController.CheckAvailebleDates", from, to);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking([FromBody]Booking booking)
        {
            try
            {
                return Ok(await _bookingRepository.Save(booking));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("BookingController.SaveBooking", booking);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody]Booking booking)
        {
            try
            {
                return Ok(await _bookingRepository.Save(id, booking));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("BookingController.UpdateBooking", id, booking);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                var booking = _bookingRepository.Delete(id);
                if (booking == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("BookingController.DeleteBooking", id);
                return BadRequest(ex.Message);
            }
        }
    }
}
