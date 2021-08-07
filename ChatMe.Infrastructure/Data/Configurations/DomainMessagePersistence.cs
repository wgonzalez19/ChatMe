namespace ChatMe.Infrastructure.Data.Configurations
{
    using ChatMe.Domain.Messages;
    using System;

    public class DomainMessagePersistence
    {
        public Guid Id { get; set; }

        public string MessageText { get; set; }

        public DateTime Timestamp { get; set; }

        public DomainUserPersistence User { get; set; }

        public static implicit operator Message(DomainMessagePersistence persistence) =>
            new(persistence.MessageText, persistence.User);
    }
}
