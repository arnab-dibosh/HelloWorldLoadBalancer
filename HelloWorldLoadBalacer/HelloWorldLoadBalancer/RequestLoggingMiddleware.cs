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
        //private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        //public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) {
        //    _next = next;
        //    _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        //}

        //public async Task Invoke(HttpContext context) {
        //    string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        //    _logger.LogInformation(
        //        "Kestrel Request=> Method:{method} {url} {Full} Status: {statusCode} Request Receive Time: {requestTime}",
        //        context.Request?.Method,
        //        context.Request?.Path.Value,
        //        context.Request.GetEncodedUrl(),
        //        context.Response?.StatusCode,
        //        requestTime);

        //    await _next(context);

        //    string responseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        //    _logger.LogInformation(
        //        "Kestrel Response=> Method:{method} {url} {Full} Status: {statusCode} Response Sent Time: {responseTime}",
        //        context.Request?.Method,
        //        context.Request?.Path.Value,
        //        context.Request.GetEncodedUrl(),
        //        context.Response?.StatusCode,
        //        responseTime);
        //}
    }
}
