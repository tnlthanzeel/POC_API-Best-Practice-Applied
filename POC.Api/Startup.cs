using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POC.Api.Middleware;
using POC.Application;
using POC.Application.Features.Schools.Command.CreateSchool;
using POC.Application.Responses;
using POC.Infrastructure;
using POC.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace POC.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddHttpCacheHeaders((expirationModelOptions) =>
        //{
        //    expirationModelOptions.MaxAge =120;
        //    expirationModelOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;

        //},
        //(validationModelOption) =>
        //{
        //    validationModelOption.MustRevalidate = true;
        //});

        //services.AddResponseCaching();

        services.AddApplicationServices();
        services.AddInfrastructureServices(Configuration);
        services.AddPersistenceServices(Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        services.AddControllers(cfg =>
        {
            cfg.CacheProfiles.Add("DefaultCache", new CacheProfile()
            {
                Duration = 240
            });
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = c =>
            {
                return new BadRequestObjectResult(new ResponseResult<object>(default(object))
                {
                    ValidationErrors = c.ModelState.Keys.Select(key => new KeyValuePair<string, IEnumerable<string>>(key, GetValue(key))).ToList()
                }); ;

                IEnumerable<string> GetValue(string key)
                {
                    var modelStateVal = c.ModelState[key];
                    //var validations = string.Join(", ", modelStateVal.Errors.Select(s => s.ErrorMessage));
                    var validations = modelStateVal.Errors.Select(s => s.ErrorMessage).ToList();

                    return validations;
                }
            };
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }).AddFluentValidation(cfg =>
        {
            cfg.ImplicitlyValidateChildProperties = true;
            cfg.RegisterValidatorsFromAssemblyContaining<CreateSchoolCommandValidator>();
        });

        services.AddFluentValidationRulesToSwagger();

        AddSwagger(services);
    }



    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //does not valdidate ETag when resource is updated
        //app.UseResponseCaching();

        //app.UseHttpCacheHeaders();

        app.UseRouting();

        app.UseCustomExceptionHandler();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Star garment POC API");
        });

        app.UseCors("Open");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddEnumsWithValuesFixFilters();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
              {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Star garment POC API",

            });
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }
}
