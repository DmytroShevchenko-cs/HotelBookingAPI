namespace HotelBooking.Web;

using System.IdentityModel.Tokens.Jwt;
using Extensions;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = CreateApplicationBuilder(args);
        
        builder.Services
            .RegisterConfigurations(builder.Configuration)
            .RegisterInfrastructureServices(builder.Configuration)
            .RegisterDatabaseAccess(builder.Configuration)
            .RegisterCustomServices()
            .RegisterIdentity(builder.Configuration)
            .RegisterSwagger();
        
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
        var app = builder.Build();
        
        app.UseStaticFiles();
        app.UseForwardedHeaders();
        app.UseExceptionHandler(_ => { });
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseConfiguredSwagger();
        
        app.MapControllers();
        
        await app.ExecuteStartupActions();
        await app.RunAsync();
    }
    
    private static WebApplicationBuilder CreateApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        return builder;
    }
}