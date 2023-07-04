using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(DbConstants.Users);

        builder.HasMany(user => user.Claims)
            .WithOne()
            .HasForeignKey(claim => claim.UserId)
            .IsRequired();

        builder.HasMany(user => user.Logins)
            .WithOne()
            .HasForeignKey(login => login.UserId)
            .IsRequired();

        builder.HasMany(user => user.Tokens)
            .WithOne()
            .HasForeignKey(token => token.UserId)
            .IsRequired();

        builder.HasMany(user => user.UserRoles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();
    }
}
