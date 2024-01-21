
using System.Text.Json;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (context == null)
            {
                throw new HttpException("context can not be null!");
            }
            await _next(context).ConfigureAwait(true);
        }
        catch (HttpException httpException)
        {
            _logger.LogError(httpException, httpException.Message);

            context.Response.ContentType = "application/json";
            var statusCode = httpException.StatusCode;
            var message = httpException.Message;

            context.Response.StatusCode = statusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(new { statusCode, message }, jsonOptions);

            await context.Response.WriteAsync(json).ConfigureAwait(true);

        }
        //catch (ValidationException validationException)
        //{
        //    _logger.LogError(validationException, validationException.Message);

        //    context.Response.ContentType = "application/json";
        //    var statusCode = 400;
        //    var message = validationException.Message ?? "validation error occurred";

        //    context.Response.StatusCode = statusCode;

        //    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        //    var json = JsonSerializer.Serialize(new { statusCode, message }, jsonOptions);

        //    await context.Response.WriteAsync(json).ConfigureAwait(true);
        //}
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";
            var statusCode = 500;
            var message = exception.Message ?? "internal server error";

            context.Response.StatusCode = statusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(new { statusCode, message }, jsonOptions);

            await context.Response.WriteAsync(json).ConfigureAwait(true);
        }
    }
}
