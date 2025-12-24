namespace HotelBooking.Web.DTOs.Rooms.UpdateRoom;

public sealed class UpdateRoomRequestDto
{
    public int RoomNumber { get; set; }
    public int HotelId { get; set; }
    public int PlaceAmount { get; set; }
    public long PricePerHour { get; set; }
}

