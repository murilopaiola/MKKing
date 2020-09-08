using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MP.MKKing.API.Errors;

namespace MP.MKKing.API.Middleware
{
    /// <summary>
    /// A middleware to globally handle exceptions
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                // If there is no exception, then the request moves into its next stage
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Attach exception StackTrace only if in Development Mode
                var response = _env.IsDevelopment() 
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                        : new ApiException((int)HttpStatusCode.InternalServerError);
                
                // Serialize our response, passing camel case json serialization option
                var json = JsonSerializer.Serialize(response, 
                    new JsonSerializerOptions
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                await context.Response.WriteAsync(json);
            }
        }
    }
}