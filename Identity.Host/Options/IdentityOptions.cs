namespace Identity.Host.Options;

internal static class IdentityOptions
{
    internal static void ConfigureHasherOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<PasswordHasherOptions>(options =>
        {
            options.IterationCount = 310_000; // OWASP recomendation
        });
    }
}
