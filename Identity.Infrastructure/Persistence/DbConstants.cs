namespace Identity.Infrastructure.Persistence;

internal static class DbConstants
{
    public const string MigrationsHistory = "ef_migrations_history";

    public const string Users = "users";
    public const string Roles = "roles";
    public const string RoleClaims = "role_claims";
    public const string UserRoles = "user_roles";
    public const string UserClaims = "user_claims";
    public const string UserLogins = "user_logins";
    public const string UserTokens = "user_tokens";
    public const string Applications = "applications";
    public const string Authorizations = "authorizations";
    public const string Tokens = "tokens";
    public const string Scopes = "scopes";
}
