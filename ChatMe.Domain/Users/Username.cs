namespace ChatMe.Domain.Users
{
    using ChatMe.Domain.Exceptions;
    using ChatMe.Resources;
    using System;
    using System.Net;

    public class Username
    {
        private const int maxUsernameLength = 50;

        public Username(string value)
        {
            Throw.When<RestException>(
                string.IsNullOrEmpty(value), 
                string.Format(ExceptionMessage.EMPTY_ARGUMENT, nameof(Username)),
                HttpStatusCode.BadRequest);

            Throw.When<RestException>(
                value.Length > maxUsernameLength, 
                string.Format(ExceptionMessage.EXCEEDS_MAX_LENGTH, maxUsernameLength.ToString()),
                HttpStatusCode.BadRequest);

            Value = value;
        }

        public string Value { get; }

        public static implicit operator Username(string value) => new(value);
    }
}
