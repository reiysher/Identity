using Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Configuration.Roles;

internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(DbConstants.Identity.RoleClaims.Table);

        builder.HasKey(claim => claim.Id);
        builder.HasIndex(claim => claim.Id)
            .IsUnique(true);

        builder.Property(claim => claim.Type);
        builder.Property(claim => claim.Value);
    }
}
