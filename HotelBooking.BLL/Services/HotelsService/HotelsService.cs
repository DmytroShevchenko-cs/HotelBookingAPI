namespace HotelBooking.BLL.Services.HotelsService;

using DAL.Commands.Hotel.CreateHotel;
using DAL.Commands.Hotel.DeleteHotel;
using DAL.Commands.Hotel.UpdateHotel;
using DAL.Queries.Hotels.GetHotels;
using Shared.Common.Result;
using MediatR;

public class HotelsService(IMediator mediator) : IHotelsService
{
    public async Task<Result<GetHotelsQueryResult>> GetHotelsAsync(GetHotelsQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result> CreateHotelAsync(CreateHotelCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> UpdateHotelAsync(UpdateHotelCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> DeleteHotelAsync(DeleteHotelCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
}

