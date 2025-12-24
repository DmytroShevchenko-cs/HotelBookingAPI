namespace HotelBooking.Web.DTOs.Bookings.GetBookingsByUserId;

public sealed class GetBookingsByUserIdRequestDto
{
    public int Offset { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}

