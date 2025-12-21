namespace HotelBooking.DAL.Database.Entities.Hotel;

using Address;
using Base;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string Street { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
    
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    
    public ICollection<Room> Rooms { get; set; } = null!;
}