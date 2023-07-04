using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class AuthorizationConfiguration : IEntityTypeConfiguration<CustomAuthorization>
{
    public void Configure(EntityTypeBuilder<CustomAuthorization> builder)
    {
        builder.ToTable(DbConstants.Authorizations);
    }
}
