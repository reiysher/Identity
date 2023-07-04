using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class TokensConfiguration : IEntityTypeConfiguration<CustomToken>
{
    public void Configure(EntityTypeBuilder<CustomToken> builder)
    {
        builder.ToTable(DbConstants.Tokens);
    }
}
