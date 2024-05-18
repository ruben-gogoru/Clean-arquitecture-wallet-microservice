namespace WebApi.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wallet_microservice_dotnet._4.Presentation.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly Dictionary<Type, IErrorHandler> _handlers;

    public ErrorHandlerMiddleware(RequestDelegate next, 
        ILogger<ErrorHandlerMiddleware> logger,
        IEnumerable<IErrorHandler> handlers)
    {
        _next = next;
        _logger = logger;

        _handlers = new Dictionary<Type, IErrorHandler>();
        // Build error handlers dictionary
        foreach (var handler in handlers)
        {
            var handlerType = handler.GetType().BaseType?.GetGenericArguments()[0];
            _handlers[handlerType] = handler;
        }
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            if (_handlers.TryGetValue(error.GetType(), out var handler))
            {
                await handler.HandleError(context, error);
            }
            else
            {
                _logger.LogError(error, error.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(error.Message );
                await context.Response.WriteAsync(result);
            }
        }
    }
}