namespace HotelBooking.DAL.Database.Entities.Booking;

using Base;
using Hotel;
using Identity;

public class Booking : BaseEntity
{
    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}