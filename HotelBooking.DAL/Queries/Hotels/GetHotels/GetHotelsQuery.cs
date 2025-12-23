namespace HotelBooking.DAL.Queries.Hotels.GetHotels;

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

public sealed class GetHotelsQuery : PaginationModel, IQuery<Result<GetHotelsQueryResult>>
{
    public int CityId { get; set; }
    public string? StreetSearch { get; set; }
    public int PlaceAmount { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int PriceFrom { get; set; }
    public int PriceTo { get; set; }
}

public class GetHotelsQueryHandler(
    ILogger<GetHotelsQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetHotelsQuery, Result<GetHotelsQueryResult>>
{
    public async Task<Result<GetHotelsQueryResult>> Handle(
        GetHotelsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Hotels
                .Where(h => !h.IsDeleted)
                .Include(h => h.City)
                .AsQueryable();

            if (request.CityId > 0)
            {
                query = query.Where(h => h.CityId == request.CityId);
            }

            if (!string.IsNullOrWhiteSpace(request.StreetSearch))
            {
                query = query.Where(h => h.Street.Contains(request.StreetSearch));
            }

            if (request.PlaceAmount > 0 || request.From != default || request.To != default || request.PriceFrom > 0 || request.PriceTo > 0)
            {
                var roomsQuery = dbContext.Rooms
                    .Where(r => !r.IsDeleted)
                    .AsQueryable();

                if (request.PlaceAmount > 0)
                {
                    roomsQuery = roomsQuery.Where(r => r.PlaceAmount >= request.PlaceAmount);
                }

                if (request.PriceFrom > 0)
                {
                    roomsQuery = roomsQuery.Where(r => r.PricePreNight >= request.PriceFrom);
                }

                if (request.PriceTo > 0)
                {
                    roomsQuery = roomsQuery.Where(r => r.PricePreNight <= request.PriceTo);
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

                    roomsQuery = roomsQuery.Where(r => !bookedRoomIds.Contains(r.Id));
                }

                var availableHotelIds = await roomsQuery
                    .Select(r => r.HotelId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                query = query.Where(h => availableHotelIds.Contains(h.Id));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(h => h.Id)
                .Skip(request.Offset)
                .Take(request.PageSize)
                .Select(h => new HotelItemModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    Street = h.Street,
                    BuildingNumber = h.BuildingNumber,
                    City = h.City.Name
                })
                .ToListAsync(cancellationToken);

            return Result<GetHotelsQueryResult>.Success(new GetHotelsQueryResult
            {
                Count = totalCount,
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetHotelsQueryHandler));
            return Result<GetHotelsQueryResult>.Failure($"Error while executing {nameof(GetHotelsQueryHandler)}");
        }
    }
}