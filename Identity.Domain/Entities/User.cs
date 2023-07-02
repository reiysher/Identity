namespace Identity.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public ICollection<UserClaim>? Claims { get; set; }

    public ICollection<UserLogin>? Logins { get; set; }

    public ICollection<UserToken>? Tokens { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}
