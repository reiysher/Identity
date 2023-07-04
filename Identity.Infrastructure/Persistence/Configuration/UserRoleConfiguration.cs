using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(DbConstants.UserRoles);

        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
        builder.HasIndex(userRole => new { userRole.UserId, userRole.RoleId })
            .IsUnique(true);
    }
}
