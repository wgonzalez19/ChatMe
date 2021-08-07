namespace ChatMe.Infrastructure.Shared
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Bot.Provider;
    using ChatMe.JWT.TokenProvider;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesConfigurations
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IBot, BotProvider>();

            return services;
        }
    }
}
