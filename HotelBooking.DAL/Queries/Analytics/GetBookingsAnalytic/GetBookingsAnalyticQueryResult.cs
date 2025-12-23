namespace HotelBooking.DAL.Queries.Analytics.GetBookingsAnalytic;

public sealed class GetBookingsAnalyticQueryResult
{
    public List<MonthlyBookingData> MonthlyData { get; set; } = null!;
}

public sealed class MonthlyBookingData
{
    public int Month { get; set; }
    public int BookingsCount { get; set; }
}