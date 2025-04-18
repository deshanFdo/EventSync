using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    // GET: api/event
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAll()
    {
        var events = await _eventRepository.GetAllAsync();
        return Ok(events);
    }

    // GET: api/event/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetById(string id)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        if (@event == null)
        {
            return NotFound();
        }
        return Ok(@event);
    }

    // GET: api/event/vendor/1
    [HttpGet("vendor/{vendorId}")]
    public async Task<ActionResult<List<Event>>> GetByVendorId(string vendorId)
    {
        var events = await _eventRepository.GetByVendorIdAsync(vendorId);
        return Ok(events);
    }

    // POST: api/event
    [HttpPost]
    public async Task<ActionResult> Create(Event @event)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _eventRepository.CreateAsync(@event);
        return CreatedAtAction(nameof(GetById), new { id = @event.Id }, @event);
    }

    // PUT: api/event/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Event @event)
    {
        if (id != @event.Id)
        {
            return BadRequest();
        }
        var existing = await _eventRepository.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }
        await _eventRepository.UpdateAsync(@event);
        return NoContent();
    }

    // DELETE: api/event/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        if (@event == null)
        {
            return NotFound();
        }
        await _eventRepository.DeleteAsync(id);
        return NoContent();
    }
}