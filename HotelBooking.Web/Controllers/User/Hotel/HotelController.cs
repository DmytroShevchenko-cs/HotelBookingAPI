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
    public async Task<IActionResult> GetHotelsAsync([FromQuery] DTOs.Hotels.GetHotels.GetHotelsRequestDto request)
    {
        var query = new GetHotelsQuery
        {
            CityId = request.CityId ?? 0,
            StreetSearch = request.StreetSearch,
            PlaceAmount = request.PlaceAmount ?? 0,
            From = request.From ?? default,
            To = request.To ?? default,
            PriceFrom = request.PriceFrom ?? 0,
            PriceTo = request.PriceTo ?? 0,
            Offset = request.Offset,
            PageSize = request.PageSize
        };
        var result = await hotelsService.GetHotelsAsync(query);
        return FromResult(result);
    }
}