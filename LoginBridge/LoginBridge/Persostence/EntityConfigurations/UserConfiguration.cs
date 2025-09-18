using LoginBridge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoginBridge.Persostence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(t => t.UserId);
        builder.Property(t => t.FirstName).IsRequired(false).HasMaxLength(100);
        builder.Property(t => t.LastName).IsRequired(false).HasMaxLength(100);
        builder.Property(t => t.Email).IsRequired();
        builder.Property(t => t.UserName).IsRequired(false).HasMaxLength(100);
        builder.Property(t => t.PasswordHash).IsRequired(false);
        builder.Property(t => t.Salt).IsRequired(false);


        builder.HasMany(x => x.LoginProviders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.Property(t => t.Role).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
    }
}
