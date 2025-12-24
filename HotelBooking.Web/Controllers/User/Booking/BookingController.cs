namespace HotelBooking.Web.Controllers.User.Booking;

using System.Security.Claims;
using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.BookingService;
using DAL.Commands.Booking.CreateBooking;
using DAL.Commands.Booking.UpdateBooking;
using DAL.Commands.Booking.DeleteBooking;
using DAL.Queries.Booking.GetBookingsByUserId;
using DAL.Queries.Booking.GetBookingsByRoomId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.User)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.User)]
public sealed class BookingController(IBookingService bookingService) : BaseApiController
{
    [HttpGet]
    [Route("api/user/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetBookingsByUserIdQueryResult>), 200)]
    public async Task<IActionResult> GetBookingsByUserIdAsync([FromQuery] DTOs.Bookings.GetBookingsByUserId.GetBookingsByUserIdRequestDto request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var query = new GetBookingsByUserIdQuery
        {
            UserId = userId,
            Offset = request.Offset,
            PageSize = request.PageSize
        };
        var result = await bookingService.GetBookingsByUserIdAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/user/rooms/{roomId}/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetBookingsByRoomIdQueryResult>), 200)]
    public async Task<IActionResult> GetBookingsByRoomIdAsync(int roomId, [FromQuery] DTOs.Bookings.GetBookingsByRoomId.GetBookingsByRoomIdRequestDto request)
    {
        var query = new GetBookingsByRoomIdQuery
        {
            RoomId = roomId
        };
        var result = await bookingService.GetBookingsByRoomIdAsync(query);
        return FromResult(result);
    }

    [HttpPost]
    [Route("api/user/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> CreateBookingAsync([FromBody] DTOs.Bookings.CreateBooking.CreateBookingRequestDto request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var command = new CreateBookingCommand(
            request.From,
            request.To,
            request.RoomId,
            userId);
        var result = await bookingService.CreateBookingAsync(command);
        return FromResult(result);
    }

    [HttpPut]
    [Route("api/user/bookings/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] DTOs.Bookings.UpdateBooking.UpdateBookingRequestDto request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var command = new UpdateBookingCommand(
            id,
            request.From,
            request.To,
            request.RoomId,
            userId,
            true);
        var result = await bookingService.UpdateBookingAsync(command);
        return FromResult(result);
    }

    [HttpDelete]
    [Route("api/user/bookings/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> DeleteBookingAsync(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var command = new DeleteBookingCommand(id, userId);
        var result = await bookingService.DeleteBookingAsync(command);
        return FromResult(result);
    }
}