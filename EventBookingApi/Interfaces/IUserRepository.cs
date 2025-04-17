using EventBookingApi.Models;

namespace EventBookingApi.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByNameAsync(string name);
    Task CreateAsync(User user);
}