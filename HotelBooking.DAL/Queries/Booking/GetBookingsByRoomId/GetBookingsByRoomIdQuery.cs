namespace HotelBooking.DAL.Queries.Booking.GetBookingsByRoomId;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetBookingsByRoomIdQuery : PaginationModel, IQuery<Result<GetBookingsByRoomIdQueryResult>>
{
}

public class GetBookingsByRoomIdQueryHandler(
    ILogger<GetBookingsByRoomIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByRoomIdQuery, Result<GetBookingsByRoomIdQueryResult>>
{
    public async Task<Result<GetBookingsByRoomIdQueryResult>> Handle(
        GetBookingsByRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<GetBookingsByRoomIdQueryResult>.Success(new GetBookingsByRoomIdQueryResult()
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetBookingsByRoomIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByRoomIdQueryHandler)}");
        }
    }
}