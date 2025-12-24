namespace HotelBooking.Web.Controllers.Admin.Booking;

using Shared.Common.Constants;
using Shared.Common.Result;
using Base;
using BLL.Services.BookingService;
using DAL.Commands.Booking.CreateBooking;
using DAL.Commands.Booking.UpdateBooking;
using DAL.Commands.Booking.DeleteBooking;
using DAL.Queries.Booking.GetBookingsByHotelId;
using DAL.Queries.Booking.GetBookingsByUserId;
using DAL.Queries.Booking.GetBookingsByRoomId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = AuthorizationConsts.Policies.Admin)]
[ApiExplorerSettings(GroupName = SwaggerConsts.Versions.Admin)]
public sealed class BookingController(IBookingService bookingService) : BaseApiController
{
    [HttpGet]
    [Route("api/admin/hotels/{hotelId}/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetBookingsByHotelIdQueryResult>), 200)]
    public async Task<IActionResult> GetBookingsByHotelIdAsync(int hotelId, [FromQuery] DTOs.Bookings.GetBookingsByHotelId.GetBookingsByHotelIdRequestDto request)
    {
        var query = new GetBookingsByHotelIdQuery
        {
            HotelId = hotelId,
            Offset = request.Offset,
            PageSize = request.PageSize
        };
        var result = await bookingService.GetBookingsByHotelIdAsync(query);
        return FromResult(result);
    }

    [HttpGet]
    [Route("api/admin/users/{userId}/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result<GetBookingsByUserIdQueryResult>), 200)]
    public async Task<IActionResult> GetBookingsByUserIdAsync(int userId, [FromQuery] DTOs.Bookings.GetBookingsByUserId.GetBookingsByUserIdRequestDto request)
    {
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
    [Route("api/admin/rooms/{roomId}/bookings")]
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
    [Route("api/admin/bookings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> CreateBookingAsync([FromBody] DTOs.Bookings.CreateBooking.CreateBookingRequestDto request)
    {
        if (!request.UserId.HasValue || request.UserId.Value <= 0)
        {
            return BadRequest("UserId is required");
        }
        var command = new CreateBookingCommand(
            request.From,
            request.To,
            request.RoomId,
            request.UserId.Value);
        var result = await bookingService.CreateBookingAsync(command);
        return FromResult(result);
    }

    [HttpPut]
    [Route("api/admin/bookings/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> UpdateBookingAsync(int id, [FromBody] DTOs.Bookings.UpdateBooking.UpdateBookingRequestDto request)
    {
        var command = new UpdateBookingCommand(
            id,
            request.From,
            request.To,
            request.RoomId,
            0,
            false);
        var result = await bookingService.UpdateBookingAsync(command);
        return FromResult(result);
    }

    [HttpDelete]
    [Route("api/admin/bookings/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> DeleteBookingAsync(int id)
    {
        var command = new DeleteBookingCommand(id);
        var result = await bookingService.DeleteBookingAsync(command);
        return FromResult(result);
    }
}