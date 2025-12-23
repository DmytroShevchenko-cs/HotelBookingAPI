namespace HotelBooking.DAL.Commands.Booking.DeleteBooking;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record DeleteBookingCommand(int Id, int? UserId = null) 
    : ICommand<Result>;

public class DeleteBookingCommandHandler(
    ILogger<DeleteBookingCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<DeleteBookingCommand, Result>
{
    public async Task<Result> Handle(
        DeleteBookingCommand request,
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

            if (request.UserId.HasValue && booking.UserId != request.UserId.Value)
            {
                return Result.Failure("You can only delete your own bookings");
            }

            dbContext.Bookings.Remove(booking);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Booking deleted!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(DeleteBookingCommand));
            return Result.Failure($"Error while executing {nameof(DeleteBookingCommand)}");
        }
    }
}

