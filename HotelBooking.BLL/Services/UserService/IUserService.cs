namespace HotelBooking.BLL.Services.UserService;

using DAL.Queries.Account.GetAccount;
using Shared.Common.Result;

public interface IUserService
{
    Task<Result<GetAccountQueryResult>> GetAccountInfoAsync(GetAccountQuery query);
}

