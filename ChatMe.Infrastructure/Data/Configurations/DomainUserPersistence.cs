namespace ChatMe.Infrastructure.Data.Configurations
{
    using ChatMe.Domain.Exceptions;
    using ChatMe.Domain.Users;
    using ChatMe.Resources;
    using System;

    public class DomainUserPersistence
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public static implicit operator User(DomainUserPersistence persistence) 
        {
            Throw.When<RestException>(
                persistence is null, 
                ExceptionMessage.INVALID_USER_PASSWORD, 
                System.Net.HttpStatusCode.Unauthorized);

            return new(persistence.Id, persistence?.Username, persistence?.Password);
        }
    }
}
