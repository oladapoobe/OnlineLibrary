using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineLibrary.Application.Exceptions;

namespace OnlineLibrary.Infrastructure.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync("Unauthorized access attempt detected.");
            }
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return context.Response.WriteAsync("Request not found.");
            }
            if (exception is ValidationException validationException)
            {

                StringBuilder errorMessageBuilder = new StringBuilder();

                foreach (var failure in validationException.Errors)
                {
                    foreach (var errormsg in failure.Value)
                    {
                        var errorMessage = $"Error: {errormsg}";
                        errorMessageBuilder.AppendLine(errorMessage);
                    }
                }
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var allErrorMessages = errorMessageBuilder.ToString();
                return context.Response.WriteAsync(allErrorMessages);
            }
            // ... handle other exception types as necessary ...

            return context.Response.WriteAsync("An error occurred while processing your request." + exception);
        }
    }
}
