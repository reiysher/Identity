namespace Identity.Domain.Constants;

public static class Messages
{
    public static class Exceptions
    {
        public const string InternalServerError = "internal_server_error";
        public const string AuthenticationFailed = "authentication_failed";
        public const string InvalidRefreshToken = "invalid_refresh_token";
        public const string InvalidToken = "invalid_token";
        public const string InvalidEmail = "invalid_email";
        public const string InvalidCountryCode = "invalid_country_code";
        public const string InvalidPhoneNumber = "invalid_phone_number";
        public const string InvalidRoleName = "invalid_role_name";
    }
}
