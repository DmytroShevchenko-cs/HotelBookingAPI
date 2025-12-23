namespace HotelBooking.BLL.Services.AnalyticsService;

using DAL.Queries.Analytics.GetBookingsAnalytic;
using DAL.Queries.Analytics.GetIncomesAnalytic;
using Shared.Common.Result;
using MediatR;

public class AnalyticsService(IMediator mediator) : IAnalyticsService
{
    public async Task<Result<GetIncomesAnalyticQueryResult>> GetIncomeAnalyticsAsync(GetIncomesAnalyticQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result<GetBookingsAnalyticQueryResult>> GetBookingAnalyticsAsync(GetBookingsAnalyticQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }
}

