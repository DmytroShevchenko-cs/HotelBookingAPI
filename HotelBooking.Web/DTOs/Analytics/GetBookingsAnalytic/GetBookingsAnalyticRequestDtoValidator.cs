namespace HotelBooking.Web.DTOs.Analytics.GetBookingsAnalytic;

using FluentValidation;

public sealed class GetBookingsAnalyticRequestDtoValidator : AbstractValidator<GetBookingsAnalyticRequestDto>
{
    public GetBookingsAnalyticRequestDtoValidator()
    {
        RuleFor(x => x.Year)
            .GreaterThanOrEqualTo(2000)
            .LessThanOrEqualTo(2100);
    }
}

