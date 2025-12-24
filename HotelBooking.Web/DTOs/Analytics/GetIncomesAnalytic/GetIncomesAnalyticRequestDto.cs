namespace HotelBooking.Web.DTOs.Analytics.GetIncomesAnalytic;

public sealed class GetIncomesAnalyticRequestDto
{
    public int Year { get; set; } = DateTime.UtcNow.Year;
}

