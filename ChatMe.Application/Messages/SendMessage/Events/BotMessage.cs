namespace ChatMe.Application.Messages.SendMessage.Events
{
    using MediatR;
    using System.Text.RegularExpressions;

    public class BotMessage : INotification
    {
        private const int botPosition = 1;

        private const int commandPosition = 2;

        private readonly Regex botCommand = new(@"^\/([\w]+)=([\w\W]+?)$");

        public BotMessage(string messageText)
        {
            MatchCollection matches = botCommand.Matches(messageText);
            this.Bot = matches[botPosition].Value;
            this.Command = matches[commandPosition].Value;
        }

        public string Bot { get; private set; }

        public string Command { get; private set; }
    }
}
