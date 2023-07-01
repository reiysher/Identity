using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Logging;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class Configure
{
    public static void EnableLogger(
        this WebApplicationBuilder builder)
    {
        builder.RegisterLogger();
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddApplicationIdentity(configuration)
            .AddPersistence();
    }

    public static async Task InitializeDatabase(this IServiceProvider services)
    {
        await services.InitializeDatabaseAsync();
    }
}
