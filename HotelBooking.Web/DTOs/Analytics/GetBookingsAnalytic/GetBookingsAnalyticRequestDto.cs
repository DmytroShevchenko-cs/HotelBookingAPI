namespace HotelBooking.Web.DTOs.Analytics.GetBookingsAnalytic;

public sealed class GetBookingsAnalyticRequestDto
{
    public int Year { get; set; } = DateTime.UtcNow.Year;
}

