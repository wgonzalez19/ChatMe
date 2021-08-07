namespace ChatMe.Application.Messages
{
    using ChatMe.Domain.Messages;
    using System;

    public class MessageDto
    {
        public string Id { get; set; }

        public string MessageText { get; set; }

        public DateTime Timestamp { get; set; }

        public string From { get; set; }

        public static MessageDto MapFromDomain(Message message)
        {
            return new MessageDto()
            {
                Id = message?.Id.ToString(),
                From = message?.From,
                MessageText = message?.MessageText,
                Timestamp = message.Timestamp,
            };
        }
    }
}
