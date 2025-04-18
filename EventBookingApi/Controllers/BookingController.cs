using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IEventRepository _eventRepository;

    public BookingController(IBookingRepository bookingRepository, IEventRepository eventRepository)
    {
        _bookingRepository = bookingRepository;
        _eventRepository = eventRepository;
    }

    // GET: api/booking/user/1
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Booking>>> GetByUserId(string userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return Ok(bookings);
    }

    // GET: api/booking/user/1/events
    [HttpGet("user/{userId}/events")]
    public async Task<ActionResult<List<Event>>> GetUserEvents(string userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        var eventIds = bookings.Select(b => b.EventId).ToList();
        var events = await _eventRepository.GetAllAsync();
        var userEvents = events.Where(e => eventIds.Contains(e.Id)).ToList();
        return Ok(userEvents);
    }

    // POST: api/booking
    [HttpPost]
    public async Task<ActionResult> Create(Booking booking)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(booking.UserId) || string.IsNullOrEmpty(booking.EventId))
        {
            return BadRequest("UserId and EventId are required.");
        }
        booking.BookedAt = DateTime.UtcNow;
        await _bookingRepository.CreateAsync(booking);
        return CreatedAtAction(nameof(GetByUserId), new { userId = booking.UserId }, booking);
    }

    // DELETE: api/booking/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var booking = await _bookingRepository.GetByUserIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        await _bookingRepository.DeleteAsync(id);
        return NoContent();
    }
}