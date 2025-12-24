namespace HotelBooking.Web.DTOs.Rooms.GetRooms;

using FluentValidation;

public sealed class GetRoomsRequestDtoValidator : AbstractValidator<GetRoomsRequestDto>
{
    public GetRoomsRequestDtoValidator()
    {
        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .When(x => x.CityId.HasValue);

        RuleFor(x => x.PlaceAmount)
            .GreaterThan(0)
            .When(x => x.PlaceAmount.HasValue);

        RuleFor(x => x.PriceFrom)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceFrom.HasValue);

        RuleFor(x => x.PriceTo)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceTo.HasValue)
            .GreaterThanOrEqualTo(x => x.PriceFrom ?? 0)
            .When(x => x.PriceTo.HasValue && x.PriceFrom.HasValue);

        RuleFor(x => x.To)
            .GreaterThanOrEqualTo(x => x.From ?? DateOnly.MinValue)
            .When(x => x.From.HasValue && x.To.HasValue);

        RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(150);
    }
}

