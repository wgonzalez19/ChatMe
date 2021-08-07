namespace ChatMe.Application.Messages.SendMessage.Events
{
    using MediatR;

    public class ChatMessage : INotification
    {
        public ChatMessage(string messageText, string username)
        {
            this.MessageText = messageText;
            this.Username = messageText;
        }

        public string MessageText { get; set; }

        public string Username { get; set; }
    }
}
