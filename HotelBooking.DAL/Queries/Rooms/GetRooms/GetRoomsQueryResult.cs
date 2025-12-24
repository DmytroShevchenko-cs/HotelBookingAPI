namespace HotelBooking.DAL.Queries.Rooms.GetRooms;


public sealed class GetRoomsQueryResult
{
    public int Count { get; set; }
    public ICollection<RoomItemModel> Items { get; set; } = null!;
}

public sealed class RoomItemModel
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int HotelId { get; set; }
    public string HotelName { get; set; } = null!;
    public int PlaceAmount { get; set; }
    public long PricePerHour { get; set; } //in cents per hour
}