using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC.Application.Features.Schools.Command.CreateSchool;
using POC.Application.Responses;

namespace POC.Api;

internal static partial class StartupHelpers
{
    public static void AddControllerConfig(this IServiceCollection services)
    {
        services.AddControllers(cfg =>
        {
            cfg.CacheProfiles.Add("DefaultCache", new CacheProfile()
            {
                Duration = 240
            });
        })
   .ConfigureApiBehaviorOptions(options =>
   {
       options.InvalidModelStateResponseFactory = c =>
       {
           return new BadRequestObjectResult(new ResponseResult()
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
   })
   .AddFluentValidation(cfg =>
   {
       cfg.ImplicitlyValidateChildProperties = true;
       cfg.RegisterValidatorsFromAssemblyContaining<CreateSchoolCommandValidator>();
   });

        services.AddFluentValidationRulesToSwagger();

        services.AddEndpointsApiExplorer();
    }

}