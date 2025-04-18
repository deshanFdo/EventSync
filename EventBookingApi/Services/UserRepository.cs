using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventBookingApi.Services;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
    {
        var settings = options.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>("Users");
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _users.Find(u => u.Name == name).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }
}
