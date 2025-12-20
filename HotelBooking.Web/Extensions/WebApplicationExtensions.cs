namespace HotelBooking.Web.Extensions;

using BLL.Services.DatabaseMigrationService;
using Infrastructure.Constants;
using Microsoft.OpenApi;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public async Task ExecuteStartupActions()
        {
            var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = serviceScopeFactory!.CreateScope();
            var migrationService = scope.ServiceProvider.GetRequiredService<IDatabaseMigrationService>();
            await migrationService.MigrateAsync();
        }

        public void UseConfiguredSwagger()
        {
            app.UseRouting();
        
            app.UseCors(SwaggerConsts.CorsPolicy);
        
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/openapi/{documentName}.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        var scheme = httpReq.Scheme;
                        swaggerDoc.Servers = new List<OpenApiServer>
                        {
                            new()
                            {
                                Url = $"{scheme}://{httpReq.Host.Value}",
                            },
                        };
                    });
                })
                .UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "swagger";

                    options.SwaggerEndpoint(
                        $"/openapi/{SwaggerConsts.ApiDocName}.json",
                        SwaggerConsts.ApiDocName);

                    options.DefaultModelExpandDepth(2);
                    options.DisplayRequestDuration();
                    options.EnableValidator();
                });
        }
    }
}