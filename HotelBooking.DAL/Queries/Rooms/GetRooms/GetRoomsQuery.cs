namespace HotelBooking.DAL.Queries.Rooms.GetRooms;

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

public sealed class GetRoomsQuery : PaginationModel, IQuery<Result<GetRoomsQueryResult>>
{
    public int CityId { get; set; }
    public string? StreetSearch { get; set; }
    public int PlaceAmount { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int PriceFrom { get; set; }
    public int PriceTo { get; set; }
}

public class GetRoomsQueryHandler(
    ILogger<GetRoomsQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetRoomsQuery, Result<GetRoomsQueryResult>>
{
    public async Task<Result<GetRoomsQueryResult>> Handle(
        GetRoomsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Rooms
                .Where(r => !r.IsDeleted)
                .Include(r => r.Hotel)
                .AsQueryable();

            if (request.CityId > 0)
            {
                query = query.Where(r => r.Hotel.CityId == request.CityId);
            }

            if (!string.IsNullOrWhiteSpace(request.StreetSearch))
            {
                query = query.Where(r => r.Hotel.Street.Contains(request.StreetSearch));
            }

            if (request.PlaceAmount > 0)
            {
                query = query.Where(r => r.PlaceAmount >= request.PlaceAmount);
            }

            if (request.PriceFrom > 0)
            {
                query = query.Where(r => r.PricePreNight >= request.PriceFrom);
            }

            if (request.PriceTo > 0)
            {
                query = query.Where(r => r.PricePreNight <= request.PriceTo);
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
                .OrderBy(r => r.Id)
                .Skip(request.Offset)
                .Take(request.PageSize)
                .Select(r => new RoomItemModel
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    HotelId = r.HotelId,
                    HotelName = r.Hotel.Name,
                    PlaceAmount = r.PlaceAmount,
                    PricePreNight = r.PricePreNight
                })
                .ToListAsync(cancellationToken);

            return Result<GetRoomsQueryResult>.Success(new GetRoomsQueryResult
            {
                Count = totalCount,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetRoomsQueryHandler));
            return Result<GetRoomsQueryResult>.Failure($"Error while executing {nameof(GetRoomsQueryHandler)}");
        }
    }
}