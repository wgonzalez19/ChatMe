namespace ChatMe.Infrastructure.Data
{
    using ChatMe.Application.Configuration.Persistence;
    using ChatMe.Infrastructure.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceExtensions
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("default"))
            );

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
