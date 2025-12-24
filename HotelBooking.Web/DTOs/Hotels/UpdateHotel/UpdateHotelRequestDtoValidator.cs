namespace HotelBooking.Web.DTOs.Hotels.UpdateHotel;

using FluentValidation;

public sealed class UpdateHotelRequestDtoValidator : AbstractValidator<UpdateHotelRequestDto>
{
    public UpdateHotelRequestDtoValidator()
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

