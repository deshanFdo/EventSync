using EventBookingApi.Interfaces;
using EventBookingApi.Models;
using EventBookingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// app.UseHttpsRedirection(); // Comment out for now
app.UseAuthorization();
app.MapControllers();

app.Run();