﻿using dating_backend.Errors;
using System.Net;
using System.Text.Json;

namespace dating_backend.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // RequestDelegate handling the next thing we need to do in our middleware.
        private readonly ILogger<ExceptionMiddleware> _logger; // Ilogger handling our exceptions messages.
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Sending the api request to our delegate.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Logging the error to our Ilogger.
                context.Response.ContentType = "application/json"; // Setting the content type to json because we are not in an api controller.
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    :
                    new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
