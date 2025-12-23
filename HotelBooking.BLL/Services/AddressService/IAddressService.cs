namespace HotelBooking.BLL.Services.AddressService;

using DAL.Queries.Address.GetCities;
using DAL.Queries.Address.GetUsedCities;
using Shared.Common.Result;

public interface IAddressService
{
    Task<Result<GetCitiesQueryResult>> GetCitiesAsync(GetCitiesQuery query);
    Task<Result<GetUsedCitiesQueryResult>> GetUsedCitiesAsync(GetUsedCitiesQuery query);
}