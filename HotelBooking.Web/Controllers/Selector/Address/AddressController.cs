namespace HotelBooking.Web.Controllers.Selector.Address;

using BLL.Services.AddressService;
using HotelBooking.DAL.Queries.Address.GetCities;
using HotelBooking.DAL.Queries.Address.GetUsedCities;
using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class AddressController(IAddressService addressService) : BaseApiController
{
    [HttpPost]
    [Produces("application/json")]
    [Route("api/selectors/cities")]
    [ProducesResponseType(typeof(Result<GetCitiesQueryResult>), 200)]
    public async Task<IActionResult> GetCitiesAsync([FromForm] GetCitiesQuery query)
    {
        var result = await addressService.GetCitiesAsync(query);
        return FromResult(result);
    }
    
    [HttpPost]
    [Produces("application/json")]
    [Route("api/selectors/cities/used")]
    [ProducesResponseType(typeof(Result<GetUsedCitiesQueryResult>), 200)]
    public async Task<IActionResult> GetUsedCitiesAsync([FromForm] GetUsedCitiesQuery query)
    {
        var result = await addressService.GetUsedCitiesAsync(query);
        return FromResult(result);
    }
}