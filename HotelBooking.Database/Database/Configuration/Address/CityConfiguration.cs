namespace HotelBooking.DAL.Database.Configuration.Address;

using Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasMany(r => r.Hotels)
            .WithOne(r => r.City)
            .HasForeignKey(r => r.CityId);
        
        builder.HasIndex(r => r.Name)
            .IsUnique();
        
        builder.Property(r => r.Name)
            .HasMaxLength(100);
    }
}