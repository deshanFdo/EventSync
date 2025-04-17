namespace EventBookingApi.Models;

public class Booking
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public DateTime BookedAt { get; set; }
}