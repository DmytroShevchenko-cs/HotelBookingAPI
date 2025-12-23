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
    public async Task<IActionResult> GetBookingsAnalyticsAsync([FromQuery] GetBookingsAnalyticQuery query)
    {
        var result = await analyticsService.GetBookingAnalyticsAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/admin/analytics/incomes")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetIncomesAnalyticQueryResult>), 200)]
    public async Task<IActionResult> GetIncomesAnalyticsAsync([FromQuery] GetIncomesAnalyticQuery query)
    {
        var result = await analyticsService.GetIncomeAnalyticsAsync(query);
        return FromResult(result);
    }
}

