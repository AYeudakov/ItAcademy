using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItAcademy.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationContextRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("WebApiDatabase"),
                b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
        });

        return services;
    }
}