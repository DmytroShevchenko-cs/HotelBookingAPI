namespace HotelBooking.DAL.Queries.Address.GetCities;

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

public sealed class GetCitiesQuery : IQuery<Result<GetCitiesQueryResult>>
{
    public string? SearchString { get; set; }
}

public class GetCitiesQueryHandler(
    ILogger<GetCitiesQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetCitiesQuery, Result<GetCitiesQueryResult>>
{
    public async Task<Result<GetCitiesQueryResult>> Handle(
        GetCitiesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Cities.AsQueryable();
            
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

            return Result<GetCitiesQueryResult>.Success(new GetCitiesQueryResult
            {
                Items = items
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetCitiesQueryResult>.Failure($"Error while executing {nameof(GetCitiesQueryHandler)}");
        }
    }
}