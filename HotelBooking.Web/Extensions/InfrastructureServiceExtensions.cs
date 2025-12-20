namespace HotelBooking.Web.Extensions;

using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configurations;
using Infrastructure.Constants;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions();
        services.AddSignalR();
        services.AddMemoryCache();
        services.AddHttpClient();
        
        var serverConfig = configuration
            .GetSection(nameof(ServerConfig))
            .Get<ServerConfig>();
        
        services.AddCors(o =>
            o.AddPolicy(SwaggerConsts.CorsPolicy, builder =>
            {
                builder.WithOrigins(serverConfig!.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition");
            }));
        
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
                options.JsonSerializerOptions.Converters.Add(NodaConverters.IntervalConverter);
                options.JsonSerializerOptions.Converters.Add(NodaConverters.InstantConverter);
                options.JsonSerializerOptions.Converters.Add(NodaConverters.LocalDateConverter);
                options.JsonSerializerOptions.Converters.Add(NodaConverters.LocalTimeConverter);
            });

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters(); 
        
        services.AddValidatorsFromAssemblyContaining<Program>();

        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        
        return services;
    }
}