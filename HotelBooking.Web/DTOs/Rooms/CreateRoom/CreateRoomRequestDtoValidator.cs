namespace HotelBooking.Web.DTOs.Rooms.CreateRoom;

using FluentValidation;

public sealed class CreateRoomRequestDtoValidator : AbstractValidator<CreateRoomRequestDto>
{
    public CreateRoomRequestDtoValidator()
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

