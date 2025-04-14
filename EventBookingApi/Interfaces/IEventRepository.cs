using EventBookingApi.Models;

namespace EventBookingApi.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<Event> GetByIdAsync(string id);
    Task CreateAsync(Event @event);
    Task UpdateAsync(string id, Event @event);
    Task DeleteAsync(string id);
}