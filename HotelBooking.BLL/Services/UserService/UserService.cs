namespace HotelBooking.BLL.Services.UserService;

using DAL.Queries.Account.GetAccount;
using Shared.Common.Result;
using MediatR;

public class UserService(IMediator mediator) : IUserService
{
    public async Task<Result<GetAccountQueryResult>> GetAccountInfoAsync(GetAccountQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }
}

