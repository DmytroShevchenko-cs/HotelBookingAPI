namespace HotelBooking.DAL.Queries.Address.GetUsedCities;


public sealed class GetUsedCitiesQueryResult
{
    public List<CityItem> Items { get; set; } = null!;
}

public sealed class CityItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}