using Identity.Infrastructure.Persistence.Contexts;
using Identity.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure.Persistence.Initialization;

namespace Identity.Infrastructure.Persistence;

internal static class Configure
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var settings = serviceProvider
                .GetRequiredService<IOptions<DatabaseSettings>>().Value;

            options.UseNpgsql(settings.ConnectionString, builder =>
            {
                builder.CommandTimeout(settings.CommandTimeoutInSeconds);
                builder.EnableRetryOnFailure();
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                builder.MigrationsHistoryTable(DbConstants.MigrationsHistory);
            });

            options.UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        var seeders = scope.ServiceProvider.GetServices<ISeeder>().OrderBy(s => s.Order);

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, cancellationToken);
        }
    }
}
