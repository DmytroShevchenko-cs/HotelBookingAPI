namespace HotelBooking.DAL.Database;

using Entities.Address;
using Entities.Booking;
using Entities.Hotel;
using Entities.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Seed;

public class BaseDbContext(DbContextOptions<BaseDbContext> options)
    : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    public DbSet<City> Cities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
        builder.AddSqlRules();
        builder.AddIdentityRules();
        builder.SeedLatest();
    }
}