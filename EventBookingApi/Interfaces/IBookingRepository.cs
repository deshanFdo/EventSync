using EventBookingApi.Models;

namespace EventBookingApi.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> GetByUserIdAsync(string userId);
    Task CreateAsync(Booking booking);
    Task DeleteAsync(string id);
}