namespace HotelBooking.BLL.Services.RoomsService;

using DAL.Commands.Rooms.CreateRoom;
using DAL.Commands.Rooms.DeleteRoom;
using DAL.Commands.Rooms.UpdateRoom;
using DAL.Queries.Rooms.GetRooms;
using DAL.Queries.Rooms.GetRoomsByHotelId;
using Shared.Common.Result;
using MediatR;

public class RoomsService(IMediator mediator) : IRoomsService
{
    public async Task<Result<GetRoomsQueryResult>> GetRoomsAsync(GetRoomsQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result<GetRoomsByHotelIdQueryResult>> GetRoomsByHotelIdAsync(GetRoomsByHotelIdQuery query)
    {
        var result = await mediator.Send(query);
        return result;
    }

    public async Task<Result> CreateRoomAsync(CreateRoomCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> UpdateRoomAsync(UpdateRoomCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }

    public async Task<Result> DeleteRoomAsync(DeleteRoomCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
}

