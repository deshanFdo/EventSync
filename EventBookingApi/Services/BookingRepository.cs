using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventBookingApi.Services;

public class BookingRepository : IBookingRepository
{
    private readonly IMongoCollection<Booking> _bookings;

    public BookingRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _bookings = database.GetCollection<Booking>("Bookings");
    }

    public async Task<List<Booking>> GetByUserIdAsync(string userId)
    {
        return await _bookings.Find(b => b.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Booking booking)
    {
        await _bookings.InsertOneAsync(booking);
    }

    public async Task DeleteAsync(string id)
    {
        await _bookings.DeleteOneAsync(b => b.Id == id);
    }
}
