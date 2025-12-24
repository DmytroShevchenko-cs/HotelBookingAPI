namespace HotelBooking.Web.DTOs.Rooms.UpdateRoom;

using FluentValidation;

public sealed class UpdateRoomRequestDtoValidator : AbstractValidator<UpdateRoomRequestDto>
{
    public UpdateRoomRequestDtoValidator()
    {
        RuleFor(x => x.RoomNumber)
            .GreaterThan(0);

        RuleFor(x => x.HotelId)
            .GreaterThan(0);

        RuleFor(x => x.PlaceAmount)
            .GreaterThan(0);

        RuleFor(x => x.PricePerHour)
            .GreaterThan(0);
    }
}

