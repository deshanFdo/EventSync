using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using EventBookingApi.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoDB settings from config
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register MongoDB client with TLS settings
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var config = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;
    var settings = MongoClientSettings.FromConnectionString(config);
    settings.SslSettings = new SslSettings 
    { 
        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 
    };
    settings.ConnectTimeout = TimeSpan.FromSeconds(60);
    return new MongoClient(settings);
});

// Register repositories (assuming they use IOptions<MongoDbSettings>)
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();

// Register MVC controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware pipeline
// app.UseHttpsRedirection(); // Disabled for Postman testing
app.UseAuthorization();
app.MapControllers();

app.Run();
