using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(DbConstants.UserTokens);
    }
}
