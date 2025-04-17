using EventBookingApi.Interfaces;
using EventBookingApi.Models;

namespace EventBookingApi.Services;

public class EventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    public Task<List<Event>> GetAllAsync()
    {
        return Task.FromResult(_events);
    }

    public Task<Event?> GetByIdAsync(string id)
    {
        var @event = _events.Find(e => e.Id == id);
        return Task.FromResult(@event);
    }

    public Task CreateAsync(Event @event)
    {
        _events.Add(@event);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Event @event)
    {
        var existing = _events.Find(e => e.Id == @event.Id);
        if (existing != null)
        {
            existing.Title = @event.Title;
            existing.Description = @event.Description;
            existing.Date = @event.Date;
            existing.Location = @event.Location;
            existing.VendorId = @event.VendorId;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string id)
    {
        var @event = _events.Find(e => e.Id == id);
        if (@event != null)
        {
            _events.Remove(@event);
        }
        return Task.CompletedTask;
    }
}