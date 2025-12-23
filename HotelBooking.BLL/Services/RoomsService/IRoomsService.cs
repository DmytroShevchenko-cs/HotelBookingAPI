namespace HotelBooking.BLL.Services.RoomsService;

using DAL.Commands.Rooms.CreateRoom;
using DAL.Commands.Rooms.DeleteRoom;
using DAL.Commands.Rooms.UpdateRoom;
using DAL.Queries.Rooms.GetRooms;
using DAL.Queries.Rooms.GetRoomsByHotelId;
using Shared.Common.Result;

public interface IRoomsService
{
    Task<Result<GetRoomsQueryResult>> GetRoomsAsync(GetRoomsQuery query);
    Task<Result<GetRoomsByHotelIdQueryResult>> GetRoomsByHotelIdAsync(GetRoomsByHotelIdQuery query);
    Task<Result> CreateRoomAsync(CreateRoomCommand command);
    Task<Result> UpdateRoomAsync(UpdateRoomCommand command);
    Task<Result> DeleteRoomAsync(DeleteRoomCommand command);
}

