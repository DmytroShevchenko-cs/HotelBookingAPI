namespace HotelBooking.Web.Controllers.User.Rooms;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.RoomsService;
using DAL.Queries.Rooms.GetRooms;
using DAL.Queries.Rooms.GetRoomsByHotelId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class RoomController(IRoomsService roomsService) : BaseApiController
{
    [HttpGet]
    [Route("api/user/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetRoomsQueryResult>), 200)]
    public async Task<IActionResult> GetRoomsAsync([FromQuery] GetRoomsQuery query)
    {
        var result = await roomsService.GetRoomsAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/user/hotels/{hotelId}/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetRoomsByHotelIdQueryResult>), 200)]
    public async Task<IActionResult> GetRoomsByHotelIdAsync(int hotelId, [FromQuery] GetRoomsByHotelIdQuery query)
    {
        query.HotelId = hotelId;
        var result = await roomsService.GetRoomsByHotelIdAsync(query);
        return FromResult(result);
    }
}