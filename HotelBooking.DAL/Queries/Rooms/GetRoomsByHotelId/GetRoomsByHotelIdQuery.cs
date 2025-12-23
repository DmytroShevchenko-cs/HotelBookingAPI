namespace HotelBooking.DAL.Queries.Rooms.GetRoomsByHotelId;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetRoomsByHotelIdQuery : PaginationModel, IQuery<Result<GetRoomsByHotelIdQueryResult>>
{
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
            return Result<GetRoomsByHotelIdQueryResult>.Success(new GetRoomsByHotelIdQueryResult()
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetRoomsByHotelIdQueryResult>.Failure($"Error while executing {nameof(GetRoomsByHotelIdQueryHandler)}");
        }
    }
}