using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Auth;

internal static class Configure
{
    internal static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddIdentity()
            .AddAuthenticationPreconfigured();
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthenticationPreconfigured(this IServiceCollection services)
    {

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            });

        return services;
    }
}
