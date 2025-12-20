namespace HotelBooking.Web.Extensions;

using Infrastructure.Configurations;

public static class ConfigurationServiceExtensions
{
    public static IServiceCollection RegisterConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ServerConfig>(configuration.GetSection(nameof(ServerConfig)));

        return services;
    }
}