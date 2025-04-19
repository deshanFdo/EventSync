using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using EventBookingApi.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Add MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = MongoClientSettings.FromConnectionString(
        builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value);
    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
    settings.ConnectTimeout = TimeSpan.FromSeconds(60);
    return new MongoClient(settings);
});

// Add services
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowFrontend");

// app.UseHttpsRedirection(); // Disabled for Postman
app.UseAuthorization();
app.MapControllers();

app.Run();