namespace HotelBooking.DAL.Queries.Booking.GetBookingsByUserId;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetBookingsByUserIdQuery : PaginationModel, IQuery<Result<GetBookingsByUserIdQueryResult>>
{
}

public class GetBookingsByUserIdQueryHandler(
    ILogger<GetBookingsByUserIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByUserIdQuery, Result<GetBookingsByUserIdQueryResult>>
{
    public async Task<Result<GetBookingsByUserIdQueryResult>> Handle(
        GetBookingsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<GetBookingsByUserIdQueryResult>.Success(new GetBookingsByUserIdQueryResult
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetBookingsByUserIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByUserIdQueryResult)}");
        }
    }
}