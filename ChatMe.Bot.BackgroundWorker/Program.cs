namespace ChatMe.Bot.BackgroundWorker
{
    using ChatMe.Bot.BackgroundWorker.Configurations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<BrokerConfiguration>(hostContext.Configuration.GetSection("Rabbit"));

                    services.AddSignalRCore();

                    services.AddHostedService<Worker>();
                });
    }
}
