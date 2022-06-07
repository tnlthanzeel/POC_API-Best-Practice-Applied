using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using POC.Application.Exceptions;
using POC.Application.Responses;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace POC.Api.Middleware
{
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

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var errorResponse = new ErrorResponse() { ValidationErrors = new List<string>() };

            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                //case ValidationException validationException:
                //    httpStatusCode = HttpStatusCode.BadRequest;
                //    errorResponse.ValidationErrors.AddRange(validationException.ValdationErrors);
                //    result = JsonConvert.SerializeObject(errorResponse);
                //    break;

                //case BadRequestException badRequestException:
                //    httpStatusCode = HttpStatusCode.BadRequest;
                //    errorResponse.ValidationErrors.Add(badRequestException.Message);
                //    result = JsonConvert.SerializeObject(errorResponse);
                //    break;

                //case NotFoundException notFoundException:
                //    httpStatusCode = HttpStatusCode.NotFound;
                //    errorResponse.ValidationErrors.Add(notFoundException.Message);
                //    result = JsonConvert.SerializeObject(errorResponse);
                //    break;

                case Exception:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.ValidationErrors.Add("Something went wrong, please try again");
                    result = JsonConvert.SerializeObject(errorResponse);
                    break;
            }

            Log.Error($"\n\n Type:\n{exception.GetType()}\n\n Message:\n{exception?.InnerException?.Message ?? exception?.Message}\n\n Stack Trace:\n{exception?.InnerException?.StackTrace ?? exception?.StackTrace}\n{new string('-', 150)}\n");

            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
