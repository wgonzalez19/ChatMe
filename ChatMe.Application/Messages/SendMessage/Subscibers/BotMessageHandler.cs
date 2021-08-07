namespace ChatMe.Application.Messages.SendMessage.Subscibers
{
    using ChatMe.Application.Configuration.Service;
    using ChatMe.Application.Messages.SendMessage.Events;
    using ChatMe.Domain.Exceptions;
    using ChatMe.Resources;
    using MediatR;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class BotMessageHandler : INotificationHandler<BotMessage>
    {
        private readonly IBot bot;

        public BotMessageHandler(IBot bot)
        {
            this.bot = bot;
        }

        public async Task Handle(BotMessage notification, CancellationToken cancellationToken)
        {
            Throw.When<RestException>(
                string.IsNullOrEmpty(notification?.Bot) || string.IsNullOrEmpty(notification?.Command),
                ExceptionMessage.INVALID_BOT_MESSAGE,
                HttpStatusCode.BadRequest);

            await this.bot.PostMessage(notification);
        }
    }
}
