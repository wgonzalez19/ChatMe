namespace ChatMe.Infrastructure.Data.Context
{
    using ChatMe.Infrastructure.Data.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options)
            : base(options) { }

        public DbSet<DomainMessagePersistence> Messages { get; set; }

        public DbSet<DomainUserPersistence> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            modelBuilder.HasDefaultSchema("ChatMe");
        }
    }
}
