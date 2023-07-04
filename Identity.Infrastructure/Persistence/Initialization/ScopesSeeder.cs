using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;

namespace Identity.Infrastructure.Persistence.Initialization;

internal class ScopesSeeder : ISeeder
{
    private readonly ILogger<ScopesSeeder> _logger;
    private readonly IOpenIddictScopeManager _scopeManager;

    public int Order => 10;

    public ScopesSeeder(ILogger<ScopesSeeder> logger,
        IOpenIddictScopeManager scopeManager)
    {
        _logger = logger;
        _scopeManager = scopeManager;

    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seeding scopes...");

        var apiScope = await _scopeManager.FindByNameAsync("identity_api", cancellationToken);

        if (apiScope != null)
        {
            await _scopeManager.DeleteAsync(apiScope, cancellationToken);
        }

        await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
        {
            DisplayName = "Identity api scope",
            Name = "identity_api",
            Resources =
                {
                    "users",
                    "roles"
                }
        }, cancellationToken);
    }
}
