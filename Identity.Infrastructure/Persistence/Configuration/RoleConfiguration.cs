using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(DbConstants.Roles);

        builder.HasMany(role => role.UserRoles)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleId)
            .IsRequired();
    }
}
