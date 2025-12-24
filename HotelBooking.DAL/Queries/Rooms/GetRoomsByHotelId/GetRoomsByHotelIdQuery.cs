namespace HotelBooking.DAL.Queries.Rooms.GetRoomsByHotelId;

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

public sealed class GetRoomsByHotelIdQuery : PaginationModel, IQuery<Result<GetRoomsByHotelIdQueryResult>>
{
    public int HotelId { get; set; }
    public int PlaceAmount { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int PriceFrom { get; set; }
    public int PriceTo { get; set; }
}

public class GetRoomsByHotelIdQueryHandler(
    ILogger<GetRoomsByHotelIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetRoomsByHotelIdQuery, Result<GetRoomsByHotelIdQueryResult>>
{
    public async Task<Result<GetRoomsByHotelIdQueryResult>> Handle(
        GetRoomsByHotelIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Rooms
                .Where(r => r.HotelId == request.HotelId && !r.IsDeleted)
                .AsQueryable();

            if (request.PlaceAmount > 0)
            {
                query = query.Where(r => r.PlaceAmount == request.PlaceAmount);
            }

            if (request.PriceFrom > 0)
            {
                query = query.Where(r => r.PricePerHour >= request.PriceFrom);
            }

            if (request.PriceTo > 0)
            {
                query = query.Where(r => r.PricePerHour <= request.PriceTo);
            }

            if (request.From != default && request.To != default)
            {
                var fromDate = new DateTimeOffset(request.From.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero);
                var toDate = new DateTimeOffset(request.To.ToDateTime(TimeOnly.MaxValue), TimeSpan.Zero);

                var bookedRoomIds = await dbContext.Bookings
                    .Where(b => b.From < toDate && b.To > fromDate)
                    .Select(b => b.RoomId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                query = query.Where(r => !bookedRoomIds.Contains(r.Id));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(r => r.RoomNumber)
                .Skip(request.Offset)
                .Take(request.PageSize)
                .Select(r => new RoomItemModel
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    PlaceAmount = r.PlaceAmount,
                    PricePerHour = r.PricePerHour
                })
                .ToListAsync(cancellationToken);

            return Result<GetRoomsByHotelIdQueryResult>.Success(new GetRoomsByHotelIdQueryResult
            {
                Count = totalCount,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetRoomsByHotelIdQueryHandler));
            return Result<GetRoomsByHotelIdQueryResult>.Failure($"Error while executing {nameof(GetRoomsByHotelIdQueryHandler)}");
        }
    }
}