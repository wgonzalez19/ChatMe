namespace ChatMe.Infrastructure.Data.Configurations
{
    using ChatMe.Domain.Messages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageConfig : IEntityTypeConfiguration<DomainMessagePersistence>
    {
        public void Configure(EntityTypeBuilder<DomainMessagePersistence> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(nameof(DomainMessagePersistence.Id));

            builder.Property(prop => prop.MessageText)
                .HasMaxLength(178)
                .IsRequired();

            builder.Property(prop => prop.Timestamp)
                .IsRequired();

            builder.Navigation(nameof(DomainMessagePersistence.User))
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .AutoInclude();
        }
    }
}
