using EventBookingApi.Models;

namespace EventBookingApi.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(string id);
    Task CreateAsync(Event @event);
    Task UpdateAsync(Event @event);
    Task DeleteAsync(string id);
    Task<List<Event>> GetByVendorIdAsync(string vendorId);
}