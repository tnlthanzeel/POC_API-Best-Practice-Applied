using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using POC.Application.Exceptions;
using POC.Application.Responses;
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
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    errorResponse.ValidationErrors.AddRange(validationException.ValdationErrors);
                    result = JsonConvert.SerializeObject(errorResponse);
                    break;

                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    errorResponse.ValidationErrors.Add(badRequestException.Message);
                    result = JsonConvert.SerializeObject(errorResponse);
                    break;

                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    errorResponse.ValidationErrors.Add(exception.Message);
                    result = JsonConvert.SerializeObject(errorResponse);
                    break;

                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.ValidationErrors.Add("Something went wrong");
                    result = JsonConvert.SerializeObject(errorResponse);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                errorResponse.ValidationErrors.Add(exception.Message);
                result = JsonConvert.SerializeObject(errorResponse);
            }

            return context.Response.WriteAsync(result);
        }
    }
}
