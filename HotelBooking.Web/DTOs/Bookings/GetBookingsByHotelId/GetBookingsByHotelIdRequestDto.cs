namespace HotelBooking.Web.DTOs.Bookings.GetBookingsByHotelId;

public sealed class GetBookingsByHotelIdRequestDto
{
    public int Offset { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}

