namespace HotelBooking.DAL.Database.Entities.Address;

using Base;
using Hotel;

public class City : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public ICollection<Hotel> Hotels { get; set; } = null!;
}