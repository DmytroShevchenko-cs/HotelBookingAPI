namespace HotelBooking.Web.DTOs.Address.GetUsedCities;

using FluentValidation;

public sealed class GetUsedCitiesRequestDtoValidator : AbstractValidator<GetUsedCitiesRequestDto>
{
    public GetUsedCitiesRequestDtoValidator()
    {
        RuleFor(x => x.SearchString)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.SearchString));
    }
}

