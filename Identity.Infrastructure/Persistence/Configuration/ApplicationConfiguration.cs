using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class ApplicationConfiguration : IEntityTypeConfiguration<CustomApplication>
{
    public void Configure(EntityTypeBuilder<CustomApplication> builder)
    {
        builder.ToTable(DbConstants.Applications);
    }
}
