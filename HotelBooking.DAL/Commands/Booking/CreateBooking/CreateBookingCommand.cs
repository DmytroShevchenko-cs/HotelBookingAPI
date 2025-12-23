namespace HotelBooking.DAL.Commands.Booking.CreateBooking;

using System;
using Database;
using Database.Entities.Booking;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record CreateBookingCommand(
    DateTimeOffset From,
    DateTimeOffset To,
    int RoomId,
    int UserId) 
    : ICommand<Result>;

public class CreateBookingCommandHandler(
    ILogger<CreateBookingCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<CreateBookingCommand, Result>
{
    public async Task<Result> Handle(
        CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
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
                    && b.From < request.To
                    && b.To > request.From, cancellationToken);
            
            if (hasOverlappingBookings)
            {
                return Result.Failure("Room is not available for the selected dates");
            }

            var booking = new Booking
            {
                From = request.From,
                To = request.To,
                RoomId = request.RoomId,
                UserId = request.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await dbContext.Bookings.AddAsync(booking, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Booking added!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(CreateBookingCommand));
            return Result.Failure($"Error while executing {nameof(CreateBookingCommand)}");
        }
    }
}