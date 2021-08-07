namespace ChatMe.Infrastructure.Data.Configurations.Seeds
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class UserSeeding : IEntityTypeConfiguration<DomainUserPersistence>
    {
        public void Configure(EntityTypeBuilder<DomainUserPersistence> builder)
        {
            builder.HasData(
                new { Id = Guid.Parse("26f40fdf-8f92-4c4f-80c1-71090d86aef4"), Username = "Will", Password = "10edaa2ccab4a337894a84fae672038702658567" },
                new { Id = Guid.Parse("1b0f8308-feb0-4d55-93ec-0765971e0bb7"), Username = "Test", Password = "10edaa2ccab4a337894a84fae672038702658567" }
            );
        }
    }
}
