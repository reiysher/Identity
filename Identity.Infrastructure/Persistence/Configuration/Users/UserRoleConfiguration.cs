using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Configuration.Users;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(DbConstants.Identity.UserRoles.Table);

        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
        builder.HasIndex(userRole => new { userRole.UserId, userRole.RoleId })
            .IsUnique(true);
    }
}
