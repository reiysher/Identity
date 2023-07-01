using Identity.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class Configure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddPersistence();
    }

    public static async Task InitializeDatabase(this IServiceProvider services)
    {
        await services.InitializeDatabaseAsync();
    }
}
