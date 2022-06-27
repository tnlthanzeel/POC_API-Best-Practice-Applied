using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using POC.Api;
using POC.Api.Middleware;
using POC.Application;
using POC.Infrastructure;
using POC.Persistence;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(builder.Configuration)
       .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"), restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
       .CreateLogger();
}

if (!builder.Environment.IsDevelopment())
{
    Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Error()
       .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"), rollingInterval: RollingInterval.Day)
       .CreateLogger();
}

builder.Host.UseSerilog();

var services = builder.Services;

services.AddApplicationServices();
services.AddInfrastructureServices(builder.Configuration);
services.AddPersistenceServices(builder.Configuration);

services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

services.AddControllerConfig();

services.AddFluentValidationRulesToSwagger();

services.AddSwagger();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC API");
});

app.UseHttpsRedirection();

app.UseCors("Open");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();