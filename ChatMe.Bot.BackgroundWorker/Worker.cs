namespace ChatMe.Bot.BackgroundWorker
{
    using ChatMe.Application.Messages;
    using ChatMe.Application.Messages.Hub;
    using ChatMe.Bot.BackgroundWorker.Configurations;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BrokerConfiguration brokerConfiguration;
        private readonly IHubContext<ChatHub, IChatHub> hub;

        public Worker(
            ILogger<Worker> logger, 
            IOptions<BrokerConfiguration> brokerConfiguration,
            IHubContext<ChatHub, IChatHub> hub
        )
        {
            _logger = logger;
            this.brokerConfiguration = brokerConfiguration.Value;
            this.hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                ConnectionFactory factory = new() { HostName = brokerConfiguration.Host };

                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: brokerConfiguration.Input, durable: false, exclusive: false, arguments: null);

                    EventingBasicConsumer consumer = new(channel);

                    consumer.Received += async (model, eventArguments) =>
                    {
                        Byte[] body = eventArguments.Body.ToArray();

                        string message = Encoding.UTF8.GetString(body);

                        await hub.Clients.All.InformClient(
                            new MessageDto()
                            {
                                MessageText = message,
                                From = "Bot",
                                Timestamp = DateTime.Now,
                                Id = Guid.NewGuid().ToString(),
                            });
                    };
                }
            }
        }
    }
}
