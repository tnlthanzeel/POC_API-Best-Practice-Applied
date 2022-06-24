using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.Application.Features.Schools.Command.CreateSchool;
using POC.Application.Responses;

namespace POC.Api;

internal static class StartupControllerConfig
{
    public static void AddControllerConfig(this IServiceCollection services)
    {
        services.AddControllers(cfg =>
        {
            cfg.ReturnHttpNotAcceptable = true;

            cfg.CacheProfiles.Add("DefaultCache", new CacheProfile()
            {
                Duration = 240
            });

            cfg.Filters.Add(new ProducesAttribute("application/json"));
            cfg.Filters.Add(new ConsumesAttribute("application/json"));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status400BadRequest));
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
       cfg.ImplicitlyValidateChildProperties = true;
       cfg.RegisterValidatorsFromAssemblyContaining<CreateSchoolCommandValidator>();
   });

        services.AddFluentValidationRulesToSwagger();

        // register the service if using Minimal APIs
        //services.AddEndpointsApiExplorer();
    }

}