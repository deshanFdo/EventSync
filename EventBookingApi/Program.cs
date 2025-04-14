using EventBookingApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Add MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Test MongoDB by saving an Event
var client = app.Services.GetService<IMongoClient>();
if (client == null)
{
    throw new Exception("MongoDB client not found.");
}
var database = client.GetDatabase(builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value);
var collection = database.GetCollection<Event>("Events");
var sampleEvent = new Event
{
    Id = "1",
    Title = "Test Event",
    Description = "Learning MongoDB Atlas",
    Date = DateTime.Now,
    Location = "Online",
    VendorId = "1"
};
collection.InsertOne(sampleEvent);

app.Run();