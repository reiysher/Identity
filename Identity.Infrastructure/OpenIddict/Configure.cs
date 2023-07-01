using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Infrastructure.OpenIddict;

internal static class Configure
{
    public static IServiceCollection AddOpenIddictPreconfigured(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddServer(options =>
            {
                // flows
                options
                    .AllowClientCredentialsFlow()
                    .AllowAuthorizationCodeFlow()
                    //.RequireProofKeyForCodeExchange()
                    .AllowRefreshTokenFlow();

                // Register scopes / permissions
                options
                    .RegisterScopes("api", Scopes.Email, Scopes.Profile, Scopes.Roles);

                // endpoints
                options
                    .SetAuthorizationEndpointUris("connect/authorize")
                    .SetLogoutEndpointUris("connect/logout")
                    .SetTokenEndpointUris("connect/token");

                // Encryption and signing of tokens
                // В этом примере мы будем использовать эфемерные ключи.
                // Временные ключи автоматически отбрасываются при завершении работы приложения,
                // а полезные данные, подписанные или зашифрованные с помощью этих ключей, автоматически аннулируются.
                // Этот метод следует использовать только во время разработки.
                // В рабочей среде рекомендуется использовать сертификат X.509.
                options
                    .AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

                options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough();
            });

        return services;
    }
}
