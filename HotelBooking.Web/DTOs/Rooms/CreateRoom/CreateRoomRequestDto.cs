namespace HotelBooking.Web.DTOs.Rooms.CreateRoom;

public sealed class CreateRoomRequestDto
{
    public int RoomNumber { get; set; }
    public int HotelId { get; set; }
    public int PlaceAmount { get; set; }
    public long PricePerHour { get; set; }
}

