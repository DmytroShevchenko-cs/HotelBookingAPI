namespace HotelBooking.DAL.Queries.Analytics.GetBookingsAnalytic;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Database.Entities.Booking;
using Database.Extensions;
using Dapper;
using MySqlConnector;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Analytics;

public sealed class GetBookingsAnalyticQuery : IQuery<Result<GetBookingsAnalyticQueryResult>>
{
    public int Year { get; set; } = DateTime.UtcNow.Year;
}

public class GetBookingsAnalyticQueryHandler(
    ILogger<GetBookingsAnalyticQueryHandler> logger,
    MySqlDataSource dataSource)
    : IRequestHandler<GetBookingsAnalyticQuery, Result<GetBookingsAnalyticQueryResult>>
{
    public async Task<Result<GetBookingsAnalyticQueryResult>> Handle(
        GetBookingsAnalyticQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = dataSource.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            var startDate = new DateTimeOffset(request.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(request.Year, 12, 31, 23, 59, 59, TimeSpan.Zero);

            var fromColumn = $"`{nameof(Booking.From).ToSnakeCase()}`";
            var bookingsTable = $"`{nameof(BaseDbContext.Bookings).ToSnakeCase()}`";
            
            var sql = $"""
                       SELECT
                           MONTH({fromColumn}) AS {nameof(BookingMonthData.Month)},
                           COUNT(*) AS {nameof(BookingMonthData.BookingsCount)}
                       FROM {bookingsTable}
                       WHERE {fromColumn} >= @StartDate
                           AND {fromColumn} <= @EndDate
                       GROUP BY MONTH({fromColumn})
                       ORDER BY MONTH({fromColumn});
                       """;

            var bookingData = await connection.QueryAsync<BookingMonthData>(
                sql,
                new { StartDate = startDate, EndDate = endDate });

            var bookingsByMonth = bookingData.ToDictionary(b => b.Month, b => b.BookingsCount);

            var monthlyData = Enumerable.Range(1, 12)
                .Select(month => new MonthlyBookingData
                {
                    Month = month,
                    BookingsCount = bookingsByMonth.GetValueOrDefault(month, 0)
                })
                .ToList();

            return Result<GetBookingsAnalyticQueryResult>.Success(new GetBookingsAnalyticQueryResult
            {
                MonthlyData = monthlyData
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetBookingsAnalyticQueryHandler));
            return Result<GetBookingsAnalyticQueryResult>.Failure(
                $"Error while executing {nameof(GetBookingsAnalyticQueryHandler)}");
        }
    }
}