namespace HotelBooking.Web.DTOs.Address.GetCities;

using FluentValidation;

public sealed class GetCitiesRequestDtoValidator : AbstractValidator<GetCitiesRequestDto>
{
    public GetCitiesRequestDtoValidator()
    {
        RuleFor(x => x.SearchString)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.SearchString));
    }
}

