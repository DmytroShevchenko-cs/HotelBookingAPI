namespace HotelBooking.Web.Extensions;

using Infrastructure.Constants;
using Microsoft.OpenApi;
using NodaTime;

public static class SwaggerServiceExtensions
{
    public static void RegisterSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(SwaggerConsts.ApiDocName, new OpenApiInfo
            {
                Version = SwaggerConsts.ApiDocName,
                Title = "Booking API",
                Description = "Booking API",
            });

            options.MapType<LocalDate>(() => new OpenApiSchema { Type = JsonSchemaType.String, Format = "date" });
            options.MapType<LocalTime>(() => new OpenApiSchema { Type = JsonSchemaType.String, Format = "time" });
            
            options.EnableAnnotations();
        });
    }
}