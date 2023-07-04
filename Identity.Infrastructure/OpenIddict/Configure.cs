using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.OpenIddict;

internal static class Configure
{
    public static IServiceCollection AddOpenIddictPreconfigured(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<CustomApplication, CustomAuthorization, CustomScope, CustomToken, Guid>();
            })
            .AddServer(options =>
            {
                options
                    .AllowClientCredentialsFlow()
                    .AllowAuthorizationCodeFlow()
                        .RequireProofKeyForCodeExchange()
                    .AllowRefreshTokenFlow();

                options
                    .SetAccessTokenLifetime(TimeSpan.FromMinutes(5))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(5))
                    .SetIdentityTokenLifetime(TimeSpan.FromMinutes(5));

                // Register scopes / permissions
                options
                    .RegisterScopes("identity_api");

                // endpoints
                options
                    .SetAuthorizationEndpointUris("/connect/authorize")
                    .SetLogoutEndpointUris("/connect/logout")
                    .SetTokenEndpointUris("/connect/token")
                    .SetUserinfoEndpointUris("/connect/userinfo");

                // Encryption and signing of tokens
                // В этом примере мы будем использовать эфемерные ключи.
                // Временные ключи автоматически отбрасываются при завершении работы приложения,
                // а полезные данные, подписанные или зашифрованные с помощью этих ключей, автоматически аннулируются.
                // Этот метод следует использовать только во время разработки.
                // В рабочей среде рекомендуется использовать сертификат X.509.
                // todo: проверить ребуты сервера с эфемерными и статичными ключами
                options
                    .AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")))
                    .AddSigningKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")))
                    .DisableAccessTokenEncryption();

                options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableVerificationEndpointPassthrough()
                    /*.DisableTransportSecurityRequirement()*/; // disable https requirement
            })
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        return services;
    }
}
