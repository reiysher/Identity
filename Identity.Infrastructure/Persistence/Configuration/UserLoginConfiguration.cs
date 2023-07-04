using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(DbConstants.UserLogins);
    }
}
