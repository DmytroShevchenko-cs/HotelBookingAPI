namespace HotelBooking.DAL.Queries.Booking.GetBookingsByRoomId;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetBookingsByRoomIdQuery : IQuery<Result<GetBookingsByRoomIdQueryResult>>
{
    public int RoomId { get; set; }
}

public class GetBookingsByRoomIdQueryHandler(
    ILogger<GetBookingsByRoomIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByRoomIdQuery, Result<GetBookingsByRoomIdQueryResult>>
{
    public async Task<Result<GetBookingsByRoomIdQueryResult>> Handle(
        GetBookingsByRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var now = DateTimeOffset.UtcNow;

            var query = dbContext.Bookings
                .Where(b => b.RoomId == request.RoomId
                    && b.To > now)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .AsQueryable();

            var items = await query
                .OrderBy(b => b.From)
                .Select(b => new BookingItemModel
                {
                    Id = b.Id,
                    From = b.From,
                    To = b.To,
                    HotelId = b.Room.HotelId,
                    HotelName = b.Room.Hotel.Name
                })
                .ToListAsync(cancellationToken);

            return Result<GetBookingsByRoomIdQueryResult>.Success(new GetBookingsByRoomIdQueryResult
            {
                Count = items.Count,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetBookingsByRoomIdQueryHandler));
            return Result<GetBookingsByRoomIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByRoomIdQueryHandler)}");
        }
    }
}