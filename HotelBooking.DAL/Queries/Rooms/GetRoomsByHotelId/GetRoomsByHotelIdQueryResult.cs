namespace HotelBooking.DAL.Queries.Rooms.GetRoomsByHotelId;


public sealed class GetRoomsByHotelIdQueryResult
{
    public int Count { get; set; }
    public ICollection<RoomItemModel> Items { get; set; } = null!;
}

public sealed class RoomItemModel
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int PlaceAmount { get; set; }
    public long PricePerHour { get; set; } //in cents per hour
}