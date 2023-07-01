using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Services;
using ItAcademy.Persistence;

namespace ItAcademy.Extensions;

public static class ServicesRegistrationExtension
{
    public static IServiceCollection AddRequiredServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IApplicationContext, ApplicationContext>();
        
        return services;
    } 
}