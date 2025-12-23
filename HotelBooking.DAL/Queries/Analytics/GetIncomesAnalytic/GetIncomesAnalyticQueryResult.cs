namespace HotelBooking.DAL.Queries.Analytics.GetIncomesAnalytic;

public sealed class GetIncomesAnalyticQueryResult
{
    public List<MonthlyIncomeData> MonthlyData { get; set; } = null!;
}

public sealed class MonthlyIncomeData
{
    public int Month { get; set; }
    public long TotalIncome { get; set; }
}