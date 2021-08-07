namespace ChatMe.Domain.Messages
{
    using ChatMe.Domain.Users;
    using System;

    public class Message
    {
        private readonly MessageText messageText;

        public Message(MessageText messageText)
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTime.Now;
            this.messageText = messageText;
        }

        public Message(MessageText messageText, User user) 
            : this(messageText)
        {
            this.User = user;
        }

        public Guid Id { get; }

        public string MessageText => messageText.Value;

        public DateTime Timestamp { get; }

        public User User { get; private set; }

        public string From => User.Username;
    }
}
