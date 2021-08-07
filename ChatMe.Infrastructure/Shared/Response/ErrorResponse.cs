namespace ChatMe.Infrastructure.Shared.Response
{
    using ChatMe.Resources;
    using System.Net;
    using System.Text.Json;

    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public string Message { get; set; } = ErrorMessage.InternalServerError;

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
