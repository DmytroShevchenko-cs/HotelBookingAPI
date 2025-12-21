namespace HotelBooking.DAL.Database.Entities.Hotel;

using Base;
using Booking;

public class Room : BaseEntity
{
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public int PlaceAmount { get; set; }
    public long PricePreNight { get; set; } //in cents

    public ICollection<Booking> Bookings { get; set; } = null!;
}