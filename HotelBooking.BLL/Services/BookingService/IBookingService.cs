namespace HotelBooking.BLL.Services.BookingService;

using DAL.Commands.Booking.CreateBooking;
using DAL.Commands.Booking.DeleteBooking;
using DAL.Commands.Booking.UpdateBooking;
using DAL.Queries.Booking.GetBookingsByHotelId;
using DAL.Queries.Booking.GetBookingsByRoomId;
using DAL.Queries.Booking.GetBookingsByUserId;
using Shared.Common.Result;

public interface IBookingService
{
    Task<Result<GetBookingsByUserIdQueryResult>> GetBookingsByUserIdAsync(GetBookingsByUserIdQuery query);
    Task<Result<GetBookingsByRoomIdQueryResult>> GetBookingsByRoomIdAsync(GetBookingsByRoomIdQuery query);
    Task<Result<GetBookingsByHotelIdQueryResult>> GetBookingsByHotelIdAsync(GetBookingsByHotelIdQuery query);
    Task<Result> CreateBookingAsync(CreateBookingCommand command);
    Task<Result> UpdateBookingAsync(UpdateBookingCommand command);
    Task<Result> DeleteBookingAsync(DeleteBookingCommand command);
}

