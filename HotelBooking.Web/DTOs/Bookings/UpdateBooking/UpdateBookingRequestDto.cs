namespace HotelBooking.Web.DTOs.Bookings.UpdateBooking;

public sealed class UpdateBookingRequestDto
{
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
    public int RoomId { get; set; }
}

