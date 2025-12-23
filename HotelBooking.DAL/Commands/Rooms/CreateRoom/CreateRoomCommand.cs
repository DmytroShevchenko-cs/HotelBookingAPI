namespace HotelBooking.DAL.Commands.Rooms.CreateRoom;

using System;
using Database;
using Database.Entities.Hotel;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record CreateRoomCommand(
    int RoomNumber,
    int HotelId,
    int PlaceAmount,
    long PricePreNight) 
    : ICommand<Result>;

public class CreateRoomCommandHandler(
    ILogger<CreateRoomCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<CreateRoomCommand, Result>
{
    public async Task<Result> Handle(
        CreateRoomCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var hotelExists = await dbContext.Hotels
                .AnyAsync(h => h.Id == request.HotelId && !h.IsDeleted, cancellationToken);
            
            if (!hotelExists)
            {
                return Result.Failure("Hotel not found");
            }

            var roomNumberExists = await dbContext.Rooms
                .AnyAsync(r => r.HotelId == request.HotelId 
                    && r.RoomNumber == request.RoomNumber 
                    && !r.IsDeleted, cancellationToken);
            
            if (roomNumberExists)
            {
                return Result.Failure("Room number already exists in this hotel");
            }

            var room = new Room
            {
                RoomNumber = request.RoomNumber,
                HotelId = request.HotelId,
                PlaceAmount = request.PlaceAmount,
                PricePreNight = request.PricePreNight,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await dbContext.Rooms.AddAsync(room, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Room added!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(CreateRoomCommand));
            return Result.Failure($"Error while executing {nameof(CreateRoomCommand)}");
        }
    }
}