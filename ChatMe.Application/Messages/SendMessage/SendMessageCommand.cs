namespace ChatMe.Application.Messages.SendMessage
{
    using ChatMe.Application.Configuration.Commands;
    using System.Collections.Generic;

    public class SendMessageCommand : CommandBase
    {
        private const string slashChar = "/";

        private readonly Dictionary<string, MessageType> typeMapper = new();
        
        public SendMessageCommand(string messageText, string username)
        {
            typeMapper.Add(slashChar, MessageType.Bot);
            typeMapper.Add(string.Empty, MessageType.Chat);

            this.MessageText = messageText;
            this.Username = username;
        }

        public string MessageText { get; }

        public string Username { get; }

        public MessageType Type => this.typeMapper[MessageText.StartsWith(slashChar) ? slashChar : string.Empty];

    }
}
