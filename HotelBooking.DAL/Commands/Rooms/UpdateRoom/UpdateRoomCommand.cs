namespace HotelBooking.DAL.Commands.Rooms.UpdateRoom;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record UpdateRoomCommand(
    int Id,
    int RoomNumber,
    int HotelId,
    int PlaceAmount,
    long PricePerHour) 
    : ICommand<Result>;

public class UpdateRoomCommandHandler(
    ILogger<UpdateRoomCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<UpdateRoomCommand, Result>
{
    public async Task<Result> Handle(
        UpdateRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var room = await dbContext.Rooms
                .FirstOrDefaultAsync(r => r.Id == request.Id && !r.IsDeleted, cancellationToken);

            if (room == null)
            {
                return Result.Failure("Room not found");
            }

            var hotelExists = await dbContext.Hotels
                .AnyAsync(h => h.Id == request.HotelId && !h.IsDeleted, cancellationToken);
            
            if (!hotelExists)
            {
                return Result.Failure("Hotel not found");
            }

            var roomNumberExists = await dbContext.Rooms
                .AnyAsync(r => r.HotelId == request.HotelId 
                    && r.RoomNumber == request.RoomNumber 
                    && r.Id != request.Id
                    && !r.IsDeleted, cancellationToken);
            
            if (roomNumberExists)
            {
                return Result.Failure("Room number already exists in this hotel");
            }

            room.RoomNumber = request.RoomNumber;
            room.HotelId = request.HotelId;
            room.PlaceAmount = request.PlaceAmount;
            room.PricePerHour = request.PricePerHour;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Room updated!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(UpdateRoomCommand));
            return Result.Failure($"Error while executing {nameof(UpdateRoomCommand)}");
        }
    }
}

