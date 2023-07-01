using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Configuration.Users;

internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(DbConstants.Identity.UserClaims.Table);

        builder.HasKey(claim => claim.Id);
        builder.HasIndex(claim => claim.Id)
            .IsUnique(true);

        builder.Property(claim => claim.Type);
        builder.Property(claim => claim.Value);
    }
}
