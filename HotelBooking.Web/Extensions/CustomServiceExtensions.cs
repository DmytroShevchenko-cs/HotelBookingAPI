namespace HotelBooking.Web.Extensions;

using BLL.Services.AddressService;
using BLL.Services.AnalyticsService;
using BLL.Services.BookingService;
using BLL.Services.HotelsService;
using BLL.Services.RoomsService;
using BLL.Services.UserService;
using DAL.Database;
using DAL.Services.DatabaseMigrationService;

public static class CustomServiceExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
             options.RegisterServicesFromAssembly(typeof(DatabaseMigrationService).Assembly);
        });

        services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHotelsService, HotelsService>();
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAddressService, AddressService>();
        
        return services;
    }
}