namespace ChatMe.Infrastructure.Data
{
    using ChatMe.Application.Configuration.Persistence;
    using ChatMe.Application.Configuration.Persistence.Repositories;
    using ChatMe.Infrastructure.Data.Context;
    using ChatMe.Infrastructure.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class PersistenceExtensions
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine($"DB Connection: --> {configuration.GetConnectionString("default")}");

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("default"))
            );

            services.AddTransient<IMessagesRepository, MessagesReporistory>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
