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
    [Route("api/user/hotels/{hotelId}/rooms")]
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
}