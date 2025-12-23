namespace HotelBooking.Web.Controllers.Admin.Hotel;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.HotelsService;
using DAL.Commands.Hotel.CreateHotel;
using DAL.Commands.Hotel.UpdateHotel;
using DAL.Commands.Hotel.DeleteHotel;
using DAL.Queries.Hotels.GetHotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class HotelController(IHotelsService hotelsService) : BaseApiController
{
    [HttpGet]
    [Route("api/admin/hotels")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetHotelsQueryResult>), 200)]
    public async Task<IActionResult> GetHotelsAsync([FromQuery] GetHotelsQuery query)
    {
        var result = await hotelsService.GetHotelsAsync(query);
        return FromResult(result);
    }

    [HttpPost]
    [Route("api/admin/hotels")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> CreateHotelAsync([FromBody] CreateHotelCommand command)
    {
        var result = await hotelsService.CreateHotelAsync(command);
        return FromResult(result);
    }

    [HttpPut]
    [Route("api/admin/hotels/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> UpdateHotelAsync(int id, [FromBody] UpdateHotelCommand command)
    {
        var updateCommand = command with { Id = id };
        var result = await hotelsService.UpdateHotelAsync(updateCommand);
        return FromResult(result);
    }

    [HttpDelete]
    [Route("api/admin/hotels/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> DeleteHotelAsync(int id)
    {
        var command = new DeleteHotelCommand(id);
        var result = await hotelsService.DeleteHotelAsync(command);
        return FromResult(result);
    }
}