using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Infrastructure.Persistence.Initialization;

internal class ApplicationsSeeder : ISeeder
{
    private readonly ILogger<ApplicationsSeeder> _logger;
    private readonly IOpenIddictApplicationManager _applicationManager;

    public int Order => 11;

    public ApplicationsSeeder(ILogger<ApplicationsSeeder> logger,
        IOpenIddictApplicationManager applicationManager)
    {
        _logger = logger;
        _applicationManager = applicationManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seeding applications...");

        var client = await _applicationManager.FindByClientIdAsync("postman", cancellationToken);
        if (client != null)
        {
            await _applicationManager.DeleteAsync(client);
        }

        await _applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "postman",
            ClientSecret = "postman_secret",
            Type = ClientTypes.Confidential,
            DisplayName = "Postman client application",
            Permissions =
            {
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.ClientCredentials,
                Permissions.GrantTypes.RefreshToken,
                Permissions.Prefixes.Scope + "identity_api",
            }
        }, cancellationToken);

        await _applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "postman2",
            Type = ClientTypes.Public,
            DisplayName = "Postman2 client application",
            RedirectUris =
            {
                new Uri("https://oauth.pstmn.io/v1/callback")
            },
            Permissions =
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.AuthorizationCode,
                Permissions.GrantTypes.RefreshToken,
                Permissions.Prefixes.Scope + "identity_api",
                Permissions.ResponseTypes.Code
            }
        }, cancellationToken);
    }
}
