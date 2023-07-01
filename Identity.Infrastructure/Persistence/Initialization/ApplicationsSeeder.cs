using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Infrastructure.Persistence.Initialization;

// todo: use ApplicationDbContext from parameter
internal class ApplicationsSeeder : ISeeder
{
    private readonly ILogger<ApplicationsSeeder> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ApplicationDbContext _dbContext;

    public int Order => 11;

    public ApplicationsSeeder(ILogger<ApplicationsSeeder> logger,
        IServiceProvider serviceProvider,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dbContext = dbContext;
    }

    public async Task SeedAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seeding applications...");

        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        var client = await manager.FindByClientIdAsync("web-client");
        if (client != null)
        {
            await manager.DeleteAsync(client);
        }

        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "web-client",
            ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
            ConsentType = ConsentTypes.Explicit,
            DisplayName = "Swagger client application",
            RedirectUris =
                {
                    new Uri("https://localhost:8006/swagger/oauth2-redirect.html")
                },
            PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:8006/resources")
                },
            Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                   $"{Permissions.Prefixes.Scope}api1"
                },
            //Requirements =
            //{
            //    Requirements.Features.ProofKeyForCodeExchange
            //}
        });
    }
}
