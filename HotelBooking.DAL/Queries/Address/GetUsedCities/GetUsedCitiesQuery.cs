namespace HotelBooking.DAL.Queries.Address.GetUsedCities;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public sealed class GetUsedCitiesQuery : IQuery<Result<GetUsedCitiesQueryResult>>
{
    public string? SearchString { get; set; }
}

public class GetUsedCitiesQueryHandler(
    ILogger<GetUsedCitiesQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetUsedCitiesQuery, Result<GetUsedCitiesQueryResult>>
{
    public async Task<Result<GetUsedCitiesQueryResult>> Handle(
        GetUsedCitiesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Hotels
                .Where(h => !h.IsDeleted)
                .Select(h => h.City)
                .Distinct()
                .AsQueryable();
            
            if(!string.IsNullOrEmpty(request.SearchString))
            {
                query = query.Where(c => c.Name.Contains(request.SearchString));
            }

            var items = await query
                .OrderBy(r => r.Id)
                .Take(10)
                .Select(c => new CityItem
                {
                    Id = c.Id,
                    City = c.Name
                })
                .ToListAsync(cancellationToken);

            return Result<GetUsedCitiesQueryResult>.Success(new GetUsedCitiesQueryResult
            {
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetUsedCitiesQueryResult>.Failure($"Error while executing {nameof(GetUsedCitiesQueryHandler)}");
        }
    }
}