namespace ChatMe.Bot.Provider
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Messages.Hub;
    using ChatMe.Application.Messages.SendMessage.Events;
    using ChatMe.Domain.Messages;
    using ChatMe.Domain.Users;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class BotProvider : IBot
    {
        private readonly IConfiguration configuration;
        private readonly IHubContext<ChatHub, IChatHub> hub;

        public BotProvider(IConfiguration configuration, IHubContext<ChatHub, IChatHub> hub)
        {
            this.configuration = configuration;
            this.hub = hub;
        }

        public async Task PostMessage(BotMessage message)
        {
            ConnectionFactory factory = new() { HostName = configuration["Rabbit:host"] };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: configuration["Rabbit:output"], durable: false, exclusive: false, arguments: null);

            string messageToBot = JsonConvert.SerializeObject(message);

            Byte[] body = Encoding.UTF8.GetBytes(messageToBot);

            channel.BasicPublish(
                exchange: string.Empty, 
                routingKey: configuration["Rabbit:output"], 
                basicProperties: null, 
                body: body);
        }

        public async Task ReceiveMessage(string message)
        {
            ConnectionFactory factory = new() { HostName = configuration["Rabbit:host"] };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: configuration["Rabbit:output"], durable: false, exclusive: false, arguments: null);

            EventingBasicConsumer consumer = new(channel);

            consumer.Received += async (model, eventArguments) =>
            {
                Byte[] body = eventArguments.Body.ToArray();

                string message = Encoding.UTF8.GetString(body);

                await hub.Clients.All.InformClient(new Message(message, new User("Bot")));
            };
        }
    }
}
