namespace HotelBooking.DAL.Database.Configuration.Hotel;

using Entities.Hotel;
using Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasOne(r => r.Hotel)
            .WithMany(r => r.Rooms)
            .HasForeignKey(r => r.HotelId);
        
        builder.HasMany(r => r.Bookings)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId);
    }
}