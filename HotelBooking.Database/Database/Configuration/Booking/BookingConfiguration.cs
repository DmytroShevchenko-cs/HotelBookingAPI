namespace HotelBooking.DAL.Database.Configuration.Booking;

using Entities.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(r => r.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(r => r.RoomId);
        
        builder.HasOne(r => r.User)
            .WithMany(r => r.Bookings)
            .HasForeignKey(r => r.UserId);
    }
}