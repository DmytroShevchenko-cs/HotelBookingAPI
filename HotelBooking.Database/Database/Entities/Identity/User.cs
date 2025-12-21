namespace HotelBooking.DAL.Database.Entities.Identity;

using Booking;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; } = null!;
    
    public ICollection<Booking> Bookings { get; set; } = null!;
}