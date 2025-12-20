namespace HotelBooking.DAL.Database.Entities.Identity;

using Booking;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; } = null!;
    
    public ICollection<Booking> Bookings { get; set; } = null!;
}