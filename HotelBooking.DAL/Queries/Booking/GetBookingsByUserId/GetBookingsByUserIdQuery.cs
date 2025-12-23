namespace HotelBooking.DAL.Queries.Booking.GetBookingsByUserId;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetBookingsByUserIdQuery : PaginationModel, IQuery<Result<GetBookingsByUserIdQueryResult>>
{
    public int UserId { get; set; }
}

public class GetBookingsByUserIdQueryHandler(
    ILogger<GetBookingsByUserIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByUserIdQuery, Result<GetBookingsByUserIdQueryResult>>
{
    public async Task<Result<GetBookingsByUserIdQueryResult>> Handle(
        GetBookingsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Bookings
                .Where(b => b.UserId == request.UserId)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(b => b.From)
                .Skip(request.Offset)
                .Take(request.PageSize)
                .Select(b => new BookingItemModel
                {
                    Id = b.Id,
                    From = b.From,
                    To = b.To,
                    RoomNumber = b.Room.RoomNumber,
                    PlaceAmount = b.Room.PlaceAmount,
                    PricePreNight = (int)b.Room.PricePreNight,
                    HotelId = b.Room.HotelId,
                    HotelName = b.Room.Hotel.Name
                })
                .ToListAsync(cancellationToken);

            return Result<GetBookingsByUserIdQueryResult>.Success(new GetBookingsByUserIdQueryResult
            {
                Count = totalCount,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetBookingsByUserIdQueryHandler));
            return Result<GetBookingsByUserIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByUserIdQueryHandler)}");
        }
    }
}