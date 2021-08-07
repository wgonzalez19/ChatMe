namespace ChatMe.Domain.Messages
{
    using ChatMe.Domain.Exceptions;
    using ChatMe.Resources;
    using System.Net;

    public class MessageText
    {
        private const int maxTextLength = 178;

        public MessageText(string value)
        {
            Throw.When<RestException>(
                string.IsNullOrEmpty(value),
                string.Format(ExceptionMessage.EMPTY_ARGUMENT, nameof(MessageText)),
                HttpStatusCode.BadRequest);

            Throw.When<RestException>(
                value.Length > maxTextLength,
                string.Format(ExceptionMessage.EXCEEDS_MAX_LENGTH, maxTextLength.ToString()),
                HttpStatusCode.BadRequest);

            Value = value;
        }

        public string Value { get; }

        public static implicit operator MessageText(string value) => new(value);
    }
}
