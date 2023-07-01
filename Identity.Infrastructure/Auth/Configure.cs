using Identity.Domain.Roles;
using Identity.Domain.Users;
using Identity.Infrastructure.Auth.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Auth;

internal static class Configure
{
    internal static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddIdentity();
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
            .AddDefaultTokenProviders();

        services.AddTransient<IUserStore<User>, UserStore>();
        services.AddTransient<IRoleStore<Role>, RoleStore>();

        return services;
    }
}
