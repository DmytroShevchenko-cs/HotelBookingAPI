namespace HotelBooking.BLL.Services.BookingService;

using DAL.Commands.Booking.CreateBooking;
using DAL.Commands.Booking.DeleteBooking;
using DAL.Commands.Booking.UpdateBooking;
using DAL.Queries.Booking.GetBookingsByHotelId;
using DAL.Queries.Booking.GetBookingsByRoomId;
using DAL.Queries.Booking.GetBookingsByUserId;
using Shared.Common.Result;
using MediatR;

public class BookingService(IMediator mediator) : IBookingService
{
    public async Task<Result<GetBookingsByUserIdQueryResult>> GetBookingsByUserIdAsync(GetBookingsByUserIdQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result<GetBookingsByRoomIdQueryResult>> GetBookingsByRoomIdAsync(GetBookingsByRoomIdQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result<GetBookingsByHotelIdQueryResult>> GetBookingsByHotelIdAsync(GetBookingsByHotelIdQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result> CreateBookingAsync(CreateBookingCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> UpdateBookingAsync(UpdateBookingCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> DeleteBookingAsync(DeleteBookingCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
}

