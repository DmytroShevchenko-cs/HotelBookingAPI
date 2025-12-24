namespace HotelBooking.Web.DTOs.Bookings.CreateBooking;

public sealed class CreateBookingRequestDto
{
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
    public int RoomId { get; set; }
    public int? UserId { get; set; }
}

