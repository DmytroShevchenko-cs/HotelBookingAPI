namespace HotelBooking.DAL.Queries.Analytics.GetIncomesAnalytic;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Database.Entities.Booking;
using Database.Entities.Hotel;
using Database.Extensions;
using Dapper;
using MySqlConnector;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Models.Analytics;

public sealed class GetIncomesAnalyticQuery : IQuery<Result<GetIncomesAnalyticQueryResult>>
{
    public int Year { get; set; } = DateTime.UtcNow.Year;
}

public class GetIncomesAnalyticQueryHandler(
    ILogger<GetIncomesAnalyticQueryHandler> logger,
    MySqlDataSource dataSource)
    : IRequestHandler<GetIncomesAnalyticQuery, Result<GetIncomesAnalyticQueryResult>>
{
    public async Task<Result<GetIncomesAnalyticQueryResult>> Handle(
        GetIncomesAnalyticQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = dataSource.CreateConnection();
            await connection.OpenAsync(cancellationToken);
            
            var startDate = new DateTimeOffset(request.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(request.Year, 12, 31, 23, 59, 59, TimeSpan.Zero);

            var sql = $"""
                SELECT
                    MONTH(b.{nameof(Booking.From).ToSnakeCase()}) AS {nameof(IncomeMonthData.Month)},
                    SUM(DATEDIFF(b.{nameof(Booking.To).ToSnakeCase()}, b.{nameof(Booking.From).ToSnakeCase()}) * r.{nameof(Room.PricePreNight).ToSnakeCase()}) AS {nameof(IncomeMonthData.TotalIncome)}
                FROM {nameof(BaseDbContext.Bookings).ToSnakeCase()} b
                INNER JOIN {nameof(BaseDbContext.Rooms).ToSnakeCase()} r ON b.{nameof(Booking.RoomId).ToSnakeCase()} = r.{nameof(Room.Id).ToSnakeCase()}
                WHERE b.{nameof(Booking.From).ToSnakeCase()} >= @StartDate
                    AND b.{nameof(Booking.From).ToSnakeCase()} <= @EndDate
                    AND r.{nameof(Room.IsDeleted).ToSnakeCase()} = 0
                GROUP BY MONTH(b.{nameof(Booking.From).ToSnakeCase()})
                ORDER BY MONTH(b.{nameof(Booking.From).ToSnakeCase()});
                """;

            var incomeData = await connection.QueryAsync<IncomeMonthData>(
                sql,
                new { StartDate = startDate, EndDate = endDate });

            var incomesByMonth = incomeData.ToDictionary(i => i.Month, i => i.TotalIncome);

            var monthlyData = Enumerable.Range(1, 12)
                .Select(month => new MonthlyIncomeData
                {
                    Month = month,
                    TotalIncome = incomesByMonth.GetValueOrDefault(month, 0)
                })
                .ToList();

            return Result<GetIncomesAnalyticQueryResult>.Success(new GetIncomesAnalyticQueryResult
            {
                MonthlyData = monthlyData
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {HandlerName}", nameof(GetIncomesAnalyticQueryHandler));
            return Result<GetIncomesAnalyticQueryResult>.Failure($"Error while executing {nameof(GetIncomesAnalyticQueryHandler)}");
        }
    }
}