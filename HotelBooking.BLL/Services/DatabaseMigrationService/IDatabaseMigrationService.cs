namespace HotelBooking.BLL.Services.DatabaseMigrationService;

public interface IDatabaseMigrationService
{
    Task MigrateAsync();
}