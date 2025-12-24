namespace HotelBooking.Web.DTOs.Bookings.CreateBooking;

using FluentValidation;

public sealed class CreateBookingRequestDtoValidator : AbstractValidator<CreateBookingRequestDto>
{
    public CreateBookingRequestDtoValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .Must(from => from >= DateTimeOffset.UtcNow)
            .WithMessage("Start date and time cannot be in the past");

        RuleFor(x => x.To)
            .NotEmpty()
            .GreaterThan(x => x.From)
            .WithMessage("End date and time must be after start date and time");

        RuleFor(x => x.RoomId)
            .GreaterThan(0);

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UserId.HasValue);
    }
}

