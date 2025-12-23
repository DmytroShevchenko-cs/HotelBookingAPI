namespace HotelBooking.DAL.Queries.Rooms.GetRooms;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetRoomsQuery : PaginationModel, IQuery<Result<GetRoomsQueryResult>>
{
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
            return Result<GetRoomsQueryResult>.Success(new GetRoomsQueryResult()
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetRoomsQueryResult>.Failure($"Error while executing {nameof(GetRoomsQueryHandler)}");
        }
    }
}