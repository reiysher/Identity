namespace Identity.Domain.Entities;

public sealed class CustomAuthorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, CustomApplication, CustomToken>
{
}
