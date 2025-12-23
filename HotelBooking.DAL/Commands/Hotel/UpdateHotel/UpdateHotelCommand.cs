namespace HotelBooking.DAL.Commands.Hotel.UpdateHotel;

using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record UpdateHotelCommand(
    int Id,
    string Name,
    string Description,
    string Street,
    string BuildingNumber,
    int CityId) 
    : ICommand<Result>;

public class UpdateHotelCommandHandler(
    ILogger<UpdateHotelCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<UpdateHotelCommand, Result>
{
    public async Task<Result> Handle(
        UpdateHotelCommand request,
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

            var cityExists = await dbContext.Cities
                .AnyAsync(c => c.Id == request.CityId, cancellationToken);
            
            if (!cityExists)
            {
                return Result.Failure("City not found");
            }

            hotel.Name = request.Name;
            hotel.Description = request.Description;
            hotel.Street = request.Street;
            hotel.BuildingNumber = request.BuildingNumber;
            hotel.CityId = request.CityId;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Hotel updated!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(UpdateHotelCommand));
            return Result.Failure($"Error while executing {nameof(UpdateHotelCommand)}");
        }
    }
}

