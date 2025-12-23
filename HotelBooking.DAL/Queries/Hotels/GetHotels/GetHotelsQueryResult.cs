namespace HotelBooking.DAL.Queries.Hotels.GetHotels;


public sealed class GetHotelsQueryResult
{
    public int Count { get; set; }
    public ICollection<HotelItemModel> Items { get; set; } = null!;
}

public sealed class HotelItemModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
    public string City { get; set; } = null!;
} 