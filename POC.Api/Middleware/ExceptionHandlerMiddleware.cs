using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using POC.Application.Responses;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace POC.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private static Task ConvertException(HttpContext context, Exception exception)
    {
        ErrorResponse errorResponse = new();

        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = string.Empty;

        switch (exception)
        {
            case Exception:
                httpStatusCode = HttpStatusCode.InternalServerError;
                errorResponse.ValidationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.InternalServerError), new[] { "Something went wrong, please try again" }));
                result = JsonConvert.SerializeObject(errorResponse);
                break;
        }

        Log.Error(SeriogTemplate(exception));

        context.Response.StatusCode = (int)httpStatusCode;

        return context.Response.WriteAsync(result);
    }

    private static string SeriogTemplate(Exception exception)
    {
        return $"\n\n Type:\n{exception.GetType()}\n\n Message:\n{exception?.InnerException?.Message ?? exception?.Message}\n\n Stack Trace:\n{exception?.InnerException?.StackTrace ?? exception?.StackTrace}\n{new string('-', 150)}\n";
    }
}
