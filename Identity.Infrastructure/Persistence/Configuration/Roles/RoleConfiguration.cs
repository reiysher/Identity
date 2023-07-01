using Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Configuration.Roles;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(DbConstants.Identity.Roles.Table);

        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id)
            .IsUnique(true);

        builder.Property(role => role.Name);
        builder.Property(role => role.Description);

        builder.HasMany(role => role.Users)
            .WithOne()
            .HasForeignKey(userRole => userRole.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(role => role.Claims)
            .WithOne()
            .HasForeignKey(claim => claim.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
