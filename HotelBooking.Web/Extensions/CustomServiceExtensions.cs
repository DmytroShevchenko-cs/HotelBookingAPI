namespace HotelBooking.Web.Extensions;

using BLL.Services.DatabaseMigrationService;
using DAL.Database;

public static class CustomServiceExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
             options.RegisterServicesFromAssembly(typeof(BaseDbContext).Assembly);
        });

        services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        
        return services;
    }
}