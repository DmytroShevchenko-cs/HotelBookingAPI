namespace HotelBooking.Web.Controllers.Admin.Rooms;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.RoomsService;
using DAL.Commands.Rooms.CreateRoom;
using DAL.Commands.Rooms.UpdateRoom;
using DAL.Commands.Rooms.DeleteRoom;
using DAL.Queries.Rooms.GetRooms;
using DAL.Queries.Rooms.GetRoomsByHotelId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class RoomController(IRoomsService roomsService) : BaseApiController
{
    [HttpGet]
    [Route("api/admin/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetRoomsQueryResult>), 200)]
    public async Task<IActionResult> GetRoomsAsync([FromQuery] GetRoomsQuery query)
    {
        var result = await roomsService.GetRoomsAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/admin/hotels/{hotelId}/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetRoomsByHotelIdQueryResult>), 200)]
    public async Task<IActionResult> GetRoomsByHotelIdAsync(int hotelId, [FromQuery] GetRoomsByHotelIdQuery query)
    {
        query.HotelId = hotelId;
        var result = await roomsService.GetRoomsByHotelIdAsync(query);
        return FromResult(result);
    }

    [HttpPost]
    [Route("api/admin/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> CreateRoomAsync([FromBody] CreateRoomCommand command)
    {
        var result = await roomsService.CreateRoomAsync(command);
        return FromResult(result);
    }

    [HttpPut]
    [Route("api/admin/rooms/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> UpdateRoomAsync(int id, [FromBody] UpdateRoomCommand command)
    {
        var updateCommand = command with { Id = id };
        var result = await roomsService.UpdateRoomAsync(updateCommand);
        return FromResult(result);
    }

    [HttpDelete]
    [Route("api/admin/rooms/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> DeleteRoomAsync(int id)
    {
        var command = new DeleteRoomCommand(id);
        var result = await roomsService.DeleteRoomAsync(command);
        return FromResult(result);
    }
}