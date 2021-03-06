namespace ChatMe.Application.Messages.SendMessage.Events
{
    using ChatMe.Domain.Exceptions;
    using ChatMe.Resources;
    using MediatR;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    public class BotMessage : INotification
    {
        private const int botPosition = 1;

        private const int commandPosition = 2;

        private readonly Regex botCommand = new(@"^\/([\w]+)=([\w\W]+?)$");

        public BotMessage(string messageText)
        {
            MatchCollection matches = botCommand.Matches(messageText);

            Throw.When<RestException>(
                matches is null, 
                ExceptionMessage.INVALID_BOT_COMMAND, 
                HttpStatusCode.BadRequest);

            this.Bot = matches.First().Groups[botPosition].Value;
            this.Command = matches.First().Groups[commandPosition].Value;
        }

        public string Bot { get; private set; }

        public string Command { get; private set; }
    }
}
