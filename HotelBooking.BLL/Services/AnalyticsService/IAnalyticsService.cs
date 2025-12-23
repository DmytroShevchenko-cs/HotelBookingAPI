namespace HotelBooking.BLL.Services.AnalyticsService;

using DAL.Queries.Analytics.GetBookingsAnalytic;
using DAL.Queries.Analytics.GetIncomesAnalytic;
using Shared.Common.Result;

public interface IAnalyticsService
{
    Task<Result<GetIncomesAnalyticQueryResult>> GetIncomeAnalyticsAsync(GetIncomesAnalyticQuery query);
    Task<Result<GetBookingsAnalyticQueryResult>> GetBookingAnalyticsAsync(GetBookingsAnalyticQuery query);
}

