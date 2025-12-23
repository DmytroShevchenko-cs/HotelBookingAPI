namespace HotelBooking.DAL.Commands.Hotel.DeleteHotel;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record DeleteHotelCommand(int Id) 
    : ICommand<Result>;

public class DeleteHotelCommandHandler(
    ILogger<DeleteHotelCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<DeleteHotelCommand, Result>
{
    public async Task<Result> Handle(
        DeleteHotelCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var hotel = await dbContext.Hotels
                .FirstOrDefaultAsync(h => h.Id == request.Id && !h.IsDeleted, cancellationToken);

            if (hotel == null)
            {
                return Result.Failure("Hotel not found");
            }

            hotel.IsDeleted = true;
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Hotel deleted!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(DeleteHotelCommand));
            return Result.Failure($"Error while executing {nameof(DeleteHotelCommand)}");
        }
    }
}

