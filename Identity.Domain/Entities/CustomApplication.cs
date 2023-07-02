namespace Identity.Domain.Entities;

public sealed class CustomApplication : OpenIddictEntityFrameworkCoreApplication<Guid, CustomAuthorization, CustomToken>
{
}
