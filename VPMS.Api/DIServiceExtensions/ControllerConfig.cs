using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VPMS.Application;
using VPMS.SharedKernel.Responses;

namespace VPMS.Api.DIServiceExtensions;

public static class ControllerConfig
{
    public static IServiceCollection AddControllerConfig(this IServiceCollection services)
    {
        services.AddControllers(cfg =>
        {
            cfg.ReturnHttpNotAcceptable = true;

            cfg.Filters.Add(new ProducesAttribute("application/json"));

            //https://github.com/dotnet/aspnetcore/issues/41060
            //cfg.Filters.Add(new ConsumesAttribute("application/json"));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status400BadRequest));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status401Unauthorized));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status403Forbidden));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status500InternalServerError));
        })
       .ConfigureApiBehaviorOptions(options =>
       {
           options.InvalidModelStateResponseFactory = c =>
           {
               return new BadRequestObjectResult(new ErrorResponse()
               {
                   Errors = c.ModelState.Keys.Select(key => new KeyValuePair<string, IEnumerable<string>>(key, GetErrorMessages(key))).ToList()
               });

               IEnumerable<string> GetErrorMessages(string key)
               {
                   var modelStateVal = c.ModelState[key];
                   var validations = modelStateVal!.Errors.Select(s => s.ErrorMessage).ToList();

                   return validations;
               }
           };
       })
       .AddNewtonsoftJson(options =>
       {
           options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

           options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
       })
       .AddFluentValidation(cfg =>
       {
           cfg.RegisterValidatorsFromAssemblyContaining<IFluentValidationAssemblyMarker>();
       });

        services.AddFluentValidationRulesToSwagger();

        return services;

    }
}
