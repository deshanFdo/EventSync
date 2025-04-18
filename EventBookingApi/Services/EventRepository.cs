using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventBookingApi.Services;

public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<Event> _events;

    public EventRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _events = database.GetCollection<Event>("Events");
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _events.Find(_ => true).ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(string id)
    {
        return await _events.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Event @event)
    {
        await _events.InsertOneAsync(@event);
    }

    public async Task UpdateAsync(Event @event)
    {
        await _events.ReplaceOneAsync(e => e.Id == @event.Id, @event);
    }

    public async Task DeleteAsync(string id)
    {
        await _events.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetByVendorIdAsync(string vendorId)
    {
        return await _events.Find(e => e.VendorId == vendorId).ToListAsync();
    }
}
