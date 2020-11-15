using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldLoadBalancer
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context) {
            string apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
            try {
                await _next(context);
            }
            finally {
                _logger.LogInformation(
                    "Kestrel=> Method:{method} {url} {Full} Status: {statusCode} Request Receive Time: {apiRequestStartTime}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request.GetEncodedUrl(),
                    context.Response?.StatusCode,
                    apiRequestStartTime);
            }
        }
    }
}
