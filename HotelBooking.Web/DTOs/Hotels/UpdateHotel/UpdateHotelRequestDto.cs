namespace HotelBooking.Web.DTOs.Hotels.UpdateHotel;

public sealed class UpdateHotelRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public int CityId { get; set; }
}

