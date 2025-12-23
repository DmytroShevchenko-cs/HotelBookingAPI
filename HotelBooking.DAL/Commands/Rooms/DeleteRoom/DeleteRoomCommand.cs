namespace HotelBooking.DAL.Commands.Rooms.DeleteRoom;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record DeleteRoomCommand(int Id) 
    : ICommand<Result>;

public class DeleteRoomCommandHandler(
    ILogger<DeleteRoomCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<DeleteRoomCommand, Result>
{
    public async Task<Result> Handle(
        DeleteRoomCommand request,
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

            var hasActiveBookings = await dbContext.Bookings
                .AnyAsync(b => b.RoomId == request.Id 
                    && b.To > DateTimeOffset.UtcNow, cancellationToken);
            
            if (hasActiveBookings)
            {
                return Result.Failure("Cannot delete room with active bookings");
            }

            room.IsDeleted = true;
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Room deleted!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(DeleteRoomCommand));
            return Result.Failure($"Error while executing {nameof(DeleteRoomCommand)}");
        }
    }
}

