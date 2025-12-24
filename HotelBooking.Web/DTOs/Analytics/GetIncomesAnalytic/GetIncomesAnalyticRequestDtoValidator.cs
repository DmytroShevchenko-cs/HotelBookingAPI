namespace HotelBooking.Web.DTOs.Analytics.GetIncomesAnalytic;

using FluentValidation;

public sealed class GetIncomesAnalyticRequestDtoValidator : AbstractValidator<GetIncomesAnalyticRequestDto>
{
    public GetIncomesAnalyticRequestDtoValidator()
    {
        RuleFor(x => x.Year)
            .GreaterThanOrEqualTo(2000)
            .LessThanOrEqualTo(2100);
    }
}

