namespace HotelBooking.Web.Controllers.Admin.Analytics;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.AnalyticsService;
using DAL.Queries.Analytics.GetBookingsAnalytic;
using DAL.Queries.Analytics.GetIncomesAnalytic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class AnalyticsController(IAnalyticsService analyticsService) : BaseApiController
{
    [HttpGet]
    [Route("api/admin/analytics/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetBookingsAnalyticQueryResult>), 200)]
    public async Task<IActionResult> GetBookingsAnalyticsAsync([FromQuery] DTOs.Analytics.GetBookingsAnalytic.GetBookingsAnalyticRequestDto request)
    {
        var query = new GetBookingsAnalyticQuery
        {
            Year = request.Year
        };
        var result = await analyticsService.GetBookingAnalyticsAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/admin/analytics/incomes")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetIncomesAnalyticQueryResult>), 200)]
    public async Task<IActionResult> GetIncomesAnalyticsAsync([FromQuery] DTOs.Analytics.GetIncomesAnalytic.GetIncomesAnalyticRequestDto request)
    {
        var query = new GetIncomesAnalyticQuery
        {
            Year = request.Year
        };
        var result = await analyticsService.GetIncomeAnalyticsAsync(query);
        return FromResult(result);
    }
}

