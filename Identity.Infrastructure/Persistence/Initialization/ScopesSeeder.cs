using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;

namespace Identity.Infrastructure.Persistence.Initialization;

internal class ScopesSeeder : ISeeder
{
    private readonly ILogger<ScopesSeeder> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ApplicationDbContext _dbContext;

    public int Order => 10;

    public ScopesSeeder(ILogger<ScopesSeeder> logger,
        IServiceProvider serviceProvider,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dbContext = dbContext;
    }

    public async Task SeedAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seeding scopes...");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        var apiScope = await manager.FindByNameAsync("api1", cancellationToken);

        if (apiScope != null)
        {
            await manager.DeleteAsync(apiScope, cancellationToken);
        }

        await manager.CreateAsync(new OpenIddictScopeDescriptor
        {
            DisplayName = "Api scope",
            Name = "api1",
            Resources =
                {
                    "resource_server_1"
                }
        }, cancellationToken);
    }
}
