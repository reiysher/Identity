namespace Identity.Domain.Entities;

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<UserRole>? UserRoles { get; set; }
}
