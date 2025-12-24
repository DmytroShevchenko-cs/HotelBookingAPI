namespace HotelBooking.Web.DTOs.Hotels.CreateHotel;

using FluentValidation;

public sealed class CreateHotelRequestDtoValidator : AbstractValidator<CreateHotelRequestDto>
{
    public CreateHotelRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.BuildingNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.CityId)
            .GreaterThan(0);
    }
}

