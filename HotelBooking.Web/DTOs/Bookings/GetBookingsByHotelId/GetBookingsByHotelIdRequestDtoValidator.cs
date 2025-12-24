namespace HotelBooking.Web.DTOs.Bookings.GetBookingsByHotelId;

using FluentValidation;

public sealed class GetBookingsByHotelIdRequestDtoValidator : AbstractValidator<GetBookingsByHotelIdRequestDto>
{
    public GetBookingsByHotelIdRequestDtoValidator()
    {
        RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(150);
    }
}

