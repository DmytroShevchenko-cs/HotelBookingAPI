namespace HotelBooking.DAL.Database.Configuration.Hotel;

using Entities.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasMany(r => r.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);
    }
};