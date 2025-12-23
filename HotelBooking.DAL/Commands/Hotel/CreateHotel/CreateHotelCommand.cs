namespace HotelBooking.DAL.Commands.Hotel.CreateHotel;

using System;
using Database;
using Database.Entities.Hotel;
using Microsoft.EntityFrameworkCore;
using Shared.Common.CQRS;
using Shared.Common.Result;
using MediatR;
using Microsoft.Extensions.Logging;

public record CreateHotelCommand(
    string Name,
    string Description,
    string Street,
    string BuildingNumber,
    int CityId) 
    : ICommand<Result>;

public class CreateHotelCommandHandler(
    ILogger<CreateHotelCommandHandler> logger,
    BaseDbContext dbContext)
    : IRequestHandler<CreateHotelCommand, Result>
{
    public async Task<Result> Handle(
        CreateHotelCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var cityExists = await dbContext.Cities
                .AnyAsync(c => c.Id == request.CityId, cancellationToken);
            
            if (!cityExists)
            {
                return Result.Failure("City not found");
            }

            var hotel = new Hotel
            {
                Name = request.Name,
                Description = request.Description,
                Street = request.Street,
                BuildingNumber = request.BuildingNumber,
                CityId = request.CityId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await dbContext.Hotels.AddAsync(hotel, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success("Hotel added!");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while executing {CommandName}", nameof(CreateHotelCommand));
            return Result.Failure($"Error while executing {nameof(CreateHotelCommand)}");
        }
    }
}