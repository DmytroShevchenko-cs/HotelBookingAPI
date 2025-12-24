namespace HotelBooking.Web.DTOs.Rooms.GetRooms;

public sealed class GetRoomsRequestDto
{
    public int? CityId { get; set; }
    public string? StreetSearch { get; set; }
    public int? PlaceAmount { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
    public int? PriceFrom { get; set; }
    public int? PriceTo { get; set; }
    public int Offset { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}

