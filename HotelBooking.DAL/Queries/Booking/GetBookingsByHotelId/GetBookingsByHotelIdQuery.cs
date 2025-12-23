namespace HotelBooking.DAL.Queries.Booking.GetBookingsByHotelId;

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

public sealed class GetBookingsByHotelIdQuery : PaginationModel, IQuery<Result<GetBookingsByHotelIdQueryResult>>
{
    public int HotelId { get; set; }
}

public class GetBookingsByHotelIdQueryHandler(
    ILogger<GetBookingsByHotelIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByHotelIdQuery, Result<GetBookingsByHotelIdQueryResult>>
{
    public async Task<Result<GetBookingsByHotelIdQueryResult>> Handle(
        GetBookingsByHotelIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Bookings
                .Where(b => b.Room.HotelId == request.HotelId)
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

            return Result<GetBookingsByHotelIdQueryResult>.Success(new GetBookingsByHotelIdQueryResult
            {
                Count = totalCount,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetBookingsByHotelIdQueryHandler));
            return Result<GetBookingsByHotelIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByHotelIdQueryHandler)}");
        }
    }
}

