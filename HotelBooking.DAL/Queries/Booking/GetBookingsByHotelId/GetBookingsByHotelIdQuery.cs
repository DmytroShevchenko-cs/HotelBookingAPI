namespace HotelBooking.DAL.Queries.Booking.GetBookingsByHotelId;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.CQRS;
using Shared.Common.Models;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class GetBookingsByHotelIdQuery : PaginationModel, IQuery<Result<GetBookingsByHotelIdQueryResult>>
{
}

public class GetBookingsByHotelIdQueryHandler(
    ILogger<GetBookingsByHotelIdQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetBookingsByHotelIdQuery, Result<GetBookingsByHotelIdQueryResult>>
{
    public async Task<Result<GetBookingsByHotelIdQueryResult>> Handle(
        GetBookingsByHotelIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<GetBookingsByHotelIdQueryResult>.Success(new GetBookingsByHotelIdQueryResult
            {
                
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetBookingsByHotelIdQueryResult>.Failure($"Error while executing {nameof(GetBookingsByHotelIdQueryHandler)}");
        }
    }
}