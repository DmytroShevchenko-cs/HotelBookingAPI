namespace HotelBooking.Web.Extensions;

using DAL.Database;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection RegisterDatabaseAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<BaseDbContext>(options =>
            options.UseMySql(
                connectionString!,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        services.AddSingleton(_ =>
        {
            var dataSourceBuilder = new MySqlDataSourceBuilder(connectionString!);
            var dataSource = dataSourceBuilder.Build();
            return dataSource;
        });

        return services;
    }
}