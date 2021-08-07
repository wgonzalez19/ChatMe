namespace ChatMe.Infrastructure.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageConfig : IEntityTypeConfiguration<DomainMessagePersistence>
    {
        public void Configure(EntityTypeBuilder<DomainMessagePersistence> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(nameof(DomainMessagePersistence.Id));

            builder.Property(message => message.MessageText)
                .HasMaxLength(178)
                .IsRequired();

            builder.Property(message => message.Timestamp)
                .IsRequired();

            builder.Navigation(nameof(DomainMessagePersistence.User))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .AutoInclude();

        }
    }
}
