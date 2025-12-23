namespace HotelBooking.DAL.Queries.Address.GetCities;


public sealed class GetCitiesQueryResult
{
    public List<CityItem> Items { get; set; } = null!;
}

public sealed class CityItem
{
    public int Id { get; set; }
    public string City { get; set; } = null!;
}