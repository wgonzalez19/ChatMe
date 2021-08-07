namespace ChatMe.Infrastructure.Data.Configurations
{
    using ChatMe.Domain.Users;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<DomainUserPersistence>
    {
        public void Configure(EntityTypeBuilder<DomainUserPersistence> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(nameof(DomainUserPersistence.Id));

            builder.Property(user => user.Username)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(user => user.Username).IsUnique();

            builder.Property(user => user.Password).IsRequired();
        }
    }
}
