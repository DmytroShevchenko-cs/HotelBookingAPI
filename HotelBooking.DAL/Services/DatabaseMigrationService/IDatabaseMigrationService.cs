namespace HotelBooking.DAL.Services.DatabaseMigrationService;

public interface IDatabaseMigrationService
{
    Task MigrateAsync();
}