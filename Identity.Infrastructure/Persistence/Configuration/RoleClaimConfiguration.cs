using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(DbConstants.RoleClaims);
    }
}
