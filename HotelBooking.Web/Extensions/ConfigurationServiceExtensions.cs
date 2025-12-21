namespace HotelBooking.Web.Extensions;

using Shared.Common.Configurations;

public static class ConfigurationServiceExtensions
{
    public static IServiceCollection RegisterConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ServerConfig>(configuration.GetSection(nameof(ServerConfig)));
        services.Configure<AdminConfig>(configuration.GetSection(nameof(AdminConfig)));

        return services;
    }
}