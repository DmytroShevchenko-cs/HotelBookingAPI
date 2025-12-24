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
    public async Task<IActionResult> GetRoomsAsync([FromQuery] DTOs.Rooms.GetRooms.GetRoomsRequestDto request)
    {
        var query = new GetRoomsQuery
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
        var result = await roomsService.GetRoomsAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/admin/hotels/{hotelId}/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetRoomsByHotelIdQueryResult>), 200)]
    public async Task<IActionResult> GetRoomsByHotelIdAsync(int hotelId, [FromQuery] DTOs.Rooms.GetRoomsByHotelId.GetRoomsByHotelIdRequestDto request)
    {
        var query = new GetRoomsByHotelIdQuery
        {
            HotelId = hotelId,
            PlaceAmount = request.PlaceAmount ?? 0,
            From = request.From ?? default,
            To = request.To ?? default,
            PriceFrom = request.PriceFrom ?? 0,
            PriceTo = request.PriceTo ?? 0,
            Offset = request.Offset,
            PageSize = request.PageSize
        };
        var result = await roomsService.GetRoomsByHotelIdAsync(query);
        return FromResult(result);
    }

    [HttpPost]
    [Route("api/admin/rooms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> CreateRoomAsync([FromBody] DTOs.Rooms.CreateRoom.CreateRoomRequestDto request)
    {
        var command = new CreateRoomCommand(
            request.RoomNumber,
            request.HotelId,
            request.PlaceAmount,
            request.PricePerHour);
        var result = await roomsService.CreateRoomAsync(command);
        return FromResult(result);
    }

    [HttpPut]
    [Route("api/admin/rooms/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> UpdateRoomAsync(int id, [FromBody] DTOs.Rooms.UpdateRoom.UpdateRoomRequestDto request)
    {
        var command = new UpdateRoomCommand(
            id,
            request.RoomNumber,
            request.HotelId,
            request.PlaceAmount,
            request.PricePerHour);
        var result = await roomsService.UpdateRoomAsync(command);
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