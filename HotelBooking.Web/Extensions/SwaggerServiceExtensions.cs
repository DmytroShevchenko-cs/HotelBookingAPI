namespace HotelBooking.Web.Extensions;

using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Shared.Common.Constants;

public static class SwaggerServiceExtensions
{
    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            
            options.SwaggerDoc(SwaggerConsts.AdminDocName, new OpenApiInfo { Title = SwaggerConsts.AdminDocName, Version = SwaggerConsts.UserDocName });
            options.SwaggerDoc(SwaggerConsts.UserDocName, new OpenApiInfo { Title = SwaggerConsts.UserDocName, Version = SwaggerConsts.UserDocName });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}