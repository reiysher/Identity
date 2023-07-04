using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class ScopesConfiguration : IEntityTypeConfiguration<CustomScope>
{
    public void Configure(EntityTypeBuilder<CustomScope> builder)
    {
        builder.ToTable(DbConstants.Scopes);
    }
}
