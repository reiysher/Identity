using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(DbConstants.UserClaims);
    }
}
