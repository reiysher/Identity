namespace Identity.Infrastructure.Persistence;

internal static class DbConstants
{
    public const string MigrationsHistory = "ef_migrations_history";

    public static class Identity
    {
        public static class Users
        {
            public const string Table = "users";

            public static class Emails
            {
                public const string Value = "user_email";
                public const string Confirmed = "email_confirmed";
            }

            public static class PhoneNumbers
            {
                public const string Value = "phone_number";
                public const string Confirmed = "phone_number_confirmed";
            }
        }

        public static class Roles
        {
            public const string Table = "roles";
        }

        public static class RoleClaims
        {
            public const string Table = "role_claims";
        }

        public static class UserRoles
        {
            public const string Table = "user_roles";
        }

        public static class UserClaims
        {
            public const string Table = "user_claims";
        }
    }
}
