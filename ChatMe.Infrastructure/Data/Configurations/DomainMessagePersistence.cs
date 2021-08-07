namespace ChatMe.Infrastructure.Data.Configurations
{
    using ChatMe.Domain.Messages;
    using ChatMe.Domain.Users;
    using System;

    public class DomainMessagePersistence
    {
        public Guid Id { get; set; }

        public string MessageText { get; set; }

        public DateTime Timestamp { get; set; }

        public DomainUserPersistence User { get; set; }

        public Guid UserId { get; set; }

        public static implicit operator Message(DomainMessagePersistence persistence)
        {
            User domainUser = new(persistence.Id, persistence.User.Username, persistence.User.Password);

            return new(persistence.MessageText, domainUser);
        }
    }
}
