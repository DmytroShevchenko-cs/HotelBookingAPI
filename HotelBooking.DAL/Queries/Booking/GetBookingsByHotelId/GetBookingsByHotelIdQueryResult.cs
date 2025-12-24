namespace HotelBooking.DAL.Queries.Booking.GetBookingsByHotelId;

public sealed class GetBookingsByHotelIdQueryResult
{
    public int Count { get; set; }
    public ICollection<BookingItemModel> Items { get; set; } = null!;
}

public sealed class BookingItemModel
{
    public int Id { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }
    
    public int RoomNumber { get; set; }
    public int PlaceAmount { get; set; }
    public int PricePerHour { get; set; }
    
    public int HotelId { get; set; }
    public string HotelName { get; set; } = null!;
}

