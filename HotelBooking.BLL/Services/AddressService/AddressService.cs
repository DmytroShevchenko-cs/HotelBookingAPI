namespace HotelBooking.BLL.Services.AddressService;

using DAL.Queries.Address.GetCities;
using DAL.Queries.Address.GetUsedCities;
using Shared.Common.Result;
using MediatR;

public class AddressService(IMediator mediator) : IAddressService
{
    public async Task<Result<GetCitiesQueryResult>> GetCitiesAsync(GetCitiesQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result<GetUsedCitiesQueryResult>> GetUsedCitiesAsync(GetUsedCitiesQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }
}