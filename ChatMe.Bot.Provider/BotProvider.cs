namespace ChatMe.Bot.Provider
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Messages.SendMessage.Events;
    using ChatMe.Bot.Provider.Models;
    using ChatMe.Domain.Exceptions;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class BotProvider : IBot
    {
        private const string queryParams = "f=sd2t2ohlcv&h&e=csv&s=";
        private readonly IConfiguration configuration;

        public BotProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task PostMessage(BotMessage message)
        {
            string url = $"{configuration["Stock:url"]}?{queryParams}{message.Command}";

            string stockResponse = await SendRequest(url);

            await QueueMessage(MapMessage(stockResponse));
        }

        private async Task<string> SendRequest(string url)
        {
            HttpClient client = new();

            var response = await client.GetStringAsync(url);
            
            return response;
        }

        private string MapMessage(string response)
        {
            var textReader = new StringReader(response);

            CsvConfiguration csvConfiguration = new(cultureInfo: CultureInfo.InvariantCulture);
            CsvReader csvReader = new(textReader, csvConfiguration);

            IEnumerable<BotResponse> records = csvReader.GetRecords<BotResponse>();

            BotResponse stockResponse = records.FirstOrDefault();

            Throw.When<RestException>(
                stockResponse is null, 
                string.Empty, 
                HttpStatusCode.NotFound);

            return $"{stockResponse.Symbol} quote is ${stockResponse.Open} per share";
        }

        private async Task QueueMessage(string message)
        {
            ConnectionFactory factory = new() { HostName = configuration["Rabbit:host"] };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: configuration["Rabbit:output"], durable: false, exclusive: false, arguments: null);

                Byte[] body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: configuration["Rabbit:output"],
                    basicProperties: properties,
                    body: body);
            }
        }
    }
}
