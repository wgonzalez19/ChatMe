namespace ChatMe.Infrastructure.Middlewares.Errors
{
    using ChatMe.Domain.Exceptions;
    using ChatMe.Infrastructure.Shared.Response;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class ExceptionHandling
    {
        private const string contentType = "application/json";

        private readonly RequestDelegate next;

        private readonly Dictionary<Type, Func<Exception, ErrorResponse>> exceptionMapper = new();

        public ExceptionHandling(RequestDelegate next)
        {
            this.next = next;

            exceptionMapper.Add(typeof(RestException), new Func<Exception, ErrorResponse>(RestException));
            exceptionMapper.Add(typeof(Exception), new Func<Exception, ErrorResponse>(InternalServerError));
            exceptionMapper.Add(typeof(SystemException), new Func<Exception, ErrorResponse>(InternalServerError));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = exceptionMapper[exception.GetType()](exception);

            context.Response.ContentType = contentType;
            context.Response.StatusCode = (int)errorResponse.StatusCode;

            await context.Response.WriteAsync(errorResponse.ToJsonString());
        }

        private ErrorResponse RestException(Exception exception)
        {
            ErrorResponse errorResponse = new();

            errorResponse.StatusCode = (exception as RestException).Code;
            errorResponse.Message = exception.Message;

            return errorResponse;
        }

        private ErrorResponse InternalServerError(Exception exception)
        {
            ErrorResponse errorResponse = new();

            errorResponse.StatusCode = HttpStatusCode.InternalServerError;
            errorResponse.Message = exception.Message;

            return errorResponse;
        }
    }
}
