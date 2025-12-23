namespace HotelBooking.BLL.Services.HotelsService;

using DAL.Commands.Hotel.CreateHotel;
using DAL.Commands.Hotel.DeleteHotel;
using DAL.Commands.Hotel.UpdateHotel;
using DAL.Queries.Hotels.GetHotels;
using Shared.Common.Result;

public interface IHotelsService
{
    Task<Result<GetHotelsQueryResult>> GetHotelsAsync(GetHotelsQuery query);
    Task<Result> CreateHotelAsync(CreateHotelCommand command);
    Task<Result> UpdateHotelAsync(UpdateHotelCommand command);
    Task<Result> DeleteHotelAsync(DeleteHotelCommand command);
}

