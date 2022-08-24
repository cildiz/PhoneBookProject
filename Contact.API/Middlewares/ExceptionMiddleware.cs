using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Contact.API.Middlewares
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var now = DateTime.UtcNow;

            _logger.LogError($"{now.ToString("HH:mm:ss")} : {ex}");

            var result = JsonConvert.SerializeObject(new ErrorResultModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = JsonConvert.SerializeObject(ex.Message)
            });
            return httpContext.Response.WriteAsync(result);
        }
    }
}
