namespace HotelBooking.Web.DTOs.Bookings.GetBookingsByUserId;

using FluentValidation;

public sealed class GetBookingsByUserIdRequestDtoValidator : AbstractValidator<GetBookingsByUserIdRequestDto>
{
    public GetBookingsByUserIdRequestDtoValidator()
    {
        RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(150);
    }
}

