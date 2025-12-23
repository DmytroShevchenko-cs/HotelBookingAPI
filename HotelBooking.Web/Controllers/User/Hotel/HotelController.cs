namespace HotelBooking.Web.Controllers.User.Hotel;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.HotelsService;
using DAL.Queries.Hotels.GetHotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class HotelController(IHotelsService hotelsService) : BaseApiController
{
    [HttpGet]
    [Route("api/user/hotels")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetHotelsQueryResult>), 200)]
    public async Task<IActionResult> GetHotelsAsync([FromQuery] GetHotelsQuery query)
    {
        var result = await hotelsService.GetHotelsAsync(query);
        return FromResult(result);
    }
}