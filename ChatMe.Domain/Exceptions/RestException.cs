namespace ChatMe.Domain.Exceptions
{
    using System;
    using System.Net;

    public class RestException : Exception
    {
        public RestException()
        {
        }

        public RestException(string message) : base(message)
        {
        }

        public RestException(HttpStatusCode code, string message) 
            : base(message)
        {
            Code = code;
        }

        public HttpStatusCode Code { get; }
    }
}
