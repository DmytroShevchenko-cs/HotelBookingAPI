namespace HotelBooking.DAL.Queries.Account.GetAccount;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Shared.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Common.CQRS;

public sealed class GetAccountQuery : IQuery<Result<GetAccountQueryResult>>
{
    public int UserId { get; set; }
}

public class GetAccountQueryHandler(
    ILogger<GetAccountQueryHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<GetAccountQuery, Result<GetAccountQueryResult>>
{
    public async Task<Result<GetAccountQueryResult>> Handle(
        GetAccountQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .Where(u => u.Id == request.UserId)
                .Select(u => new GetAccountQueryResult
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!
                })
                .FirstOrDefaultAsync(cancellationToken);

            return user == null ? 
                Result<GetAccountQueryResult>.Failure("User not found") : 
                Result<GetAccountQueryResult>.Success(user);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return Result<GetAccountQueryResult>.Failure($"Error while executing {nameof(GetAccountQueryHandler)}");
        }
    }
}