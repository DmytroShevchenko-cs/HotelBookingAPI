namespace HotelBooking.DAL.Database.Configuration.Address;

using Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasIndex(r => r.Name)
            .IsUnique();
        
        builder.HasMany(r => r.Cities)
            .WithOne(c => c.Country)
            .HasForeignKey(r => r.CountryId);
        
        builder.Property(r => r.Name)
            .HasMaxLength(100);
    }
}