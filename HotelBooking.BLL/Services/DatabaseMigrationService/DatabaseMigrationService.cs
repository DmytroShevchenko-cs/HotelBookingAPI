namespace HotelBooking.BLL.Services.DatabaseMigrationService;

using DAL.Database;
using Microsoft.EntityFrameworkCore;

public class DatabaseMigrationService : IDatabaseMigrationService
{
    private readonly BaseDbContext _dbContext;

    public DatabaseMigrationService(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task MigrateAsync()
    {
        await _dbContext.Database.MigrateAsync();
    }
}