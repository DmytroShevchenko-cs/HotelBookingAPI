namespace HotelBooking.DAL.Database.Entities.Address;

using Base;

public class Country : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = null!;
}