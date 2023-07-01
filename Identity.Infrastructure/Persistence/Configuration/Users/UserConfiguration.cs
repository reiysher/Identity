using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Configuration.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(DbConstants.Identity.Users.Table);

        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id)
            .IsUnique(true);

        builder.Property(user => user.UserName);
        builder.Property(user => user.PasswordHash);

        builder.OwnsOne(user => user.Email, navigationBuilder =>
        {
            navigationBuilder.HasIndex(email => email.Value)
                .IsUnique(true);

            navigationBuilder.Property(email => email.Value)
                .HasColumnName(DbConstants.Identity.Users.Emails.Value);
            navigationBuilder.Property(email => email.Confirmed)
                .HasColumnName(DbConstants.Identity.Users.Emails.Confirmed);
        });

        builder.OwnsOne(user => user.PhoneNumber, navigationBuilder =>
        {
            navigationBuilder.HasIndex(phoneNumber => phoneNumber.Value)
                .IsUnique(true);

            navigationBuilder.Property(phoneNumber => phoneNumber.Value)
                .HasColumnName(DbConstants.Identity.Users.PhoneNumbers.Value);
            navigationBuilder.Property(phoneNumber => phoneNumber.Confirmed)
                .HasColumnName(DbConstants.Identity.Users.PhoneNumbers.Confirmed);
        });

        builder.HasMany(user => user.Roles)
            .WithOne()
            .HasForeignKey(userRole => userRole.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(role => role.Claims)
            .WithOne()
            .HasForeignKey(claim => claim.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
