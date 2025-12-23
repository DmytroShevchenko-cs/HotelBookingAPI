namespace HotelBooking.DAL.Commands.Booking.UpdateBooking;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record UpdateBookingCommand(
    int Id,
    DateTimeOffset From,
    DateTimeOffset To,
    int RoomId,
    int UserId) 
    : ICommand<Result>;

public class UpdateBookingCommandHandler(
    ILogger<UpdateBookingCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<UpdateBookingCommand, Result>
{
    public async Task<Result> Handle(
        UpdateBookingCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var booking = await dbContext.Bookings
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (booking == null)
            {
                return Result.Failure("Booking not found");
            }

            if (request.From >= request.To)
            {
                return Result.Failure("Check-out date must be after check-in date");
            }

            if (request.From < DateTimeOffset.UtcNow)
            {
                return Result.Failure("Check-in date cannot be in the past");
            }

            var roomExists = await dbContext.Rooms
                .AnyAsync(r => r.Id == request.RoomId && !r.IsDeleted, cancellationToken);
            
            if (!roomExists)
            {
                return Result.Failure("Room not found");
            }

            var userExists = await dbContext.Users
                .AnyAsync(u => u.Id == request.UserId, cancellationToken);
            
            if (!userExists)
            {
                return Result.Failure("User not found");
            }

            var hasOverlappingBookings = await dbContext.Bookings
                .AnyAsync(b => b.RoomId == request.RoomId
                    && b.Id != request.Id
                    && b.From < request.To
                    && b.To > request.From, cancellationToken);
            
            if (hasOverlappingBookings)
            {
                return Result.Failure("Room is not available for the selected dates");
            }

            booking.From = request.From;
            booking.To = request.To;
            booking.RoomId = request.RoomId;
            booking.UserId = request.UserId;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Booking updated!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(UpdateBookingCommand));
            return Result.Failure($"Error while executing {nameof(UpdateBookingCommand)}");
        }
    }
}

