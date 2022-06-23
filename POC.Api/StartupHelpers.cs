using Microsoft.OpenApi.Models;
using POC.Application.Features.Users.Queries.GetUserDetail;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace POC.Api;

internal static partial class StartupHelpers
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddEnumsWithValuesFixFilters();
            c.EnableAnnotations();
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
                Title = "POC API",

            });

            var apiCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiCommentsFullPath = Path.Combine(AppContext.BaseDirectory, apiCommentsFile);

            c.IncludeXmlComments(apiCommentsFullPath);

            var applicationCommentsFile = $"{typeof(UserDetailViewModel).Assembly.GetName().Name}.xml";
            var applicationCommentsFullPath = Path.Combine(AppContext.BaseDirectory, applicationCommentsFile);

            c.IncludeXmlComments(applicationCommentsFullPath);
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }
}