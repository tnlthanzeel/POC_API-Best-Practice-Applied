using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POC.Api.Middleware;
using POC.Application;
using POC.Infrastructure;
using POC.Persistence;

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

        services.AddControllerConfig();

        services.AddFluentValidationRulesToSwagger();

        services.AddSwagger();
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
}
