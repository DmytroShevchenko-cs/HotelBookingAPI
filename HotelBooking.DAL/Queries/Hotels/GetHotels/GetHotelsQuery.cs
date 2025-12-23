namespace HotelBooking.DAL.Queries.Hotels.GetHotels;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetHotelsQuery : PaginationModel, IQuery<Result<GetHotelsQueryResult>>
{
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
            return Result<GetHotelsQueryResult>.Success(new GetHotelsQueryResult
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetHotelsQueryResult>.Failure($"Error while executing {nameof(GetHotelsQuery)}");
        }
    }
}