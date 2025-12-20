namespace HotelBooking.DAL.Database.Entities.Hotel;

using Base;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<Room> Rooms { get; set; } = null!;
}