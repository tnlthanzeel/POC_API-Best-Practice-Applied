using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VPMS.Api.DIServiceExtensions;
using VPMS.Api.Middleware;
using VPMS.Api.Services;
using VPMS.Application;
using VPMS.Infrastructure;
using VPMS.Persistence;
using VPMS.SharedKernel.Interfaces;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddSerilogConfig();

    builder.Host.UseSerilog();

    // Add services to the container.
    var services = builder.Services;

    services.AddApplicationInsightsTelemetry();

    services.AddControllerConfig();

    services.AddSwaggerConfig();

    services.AddCorsConfig();

    services.AddApplicationServices();
    services.AddInfrastructureServices(builder.Configuration);
    services.AddPersistenceServices(builder.Configuration);

    services.AddHttpContextAccessor();
    services.AddScoped<ILoggedInUserService, LoggedInUserService>();

    services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

    services.AddIdentityConfig(builder);
}

var app = builder.Build();
{
    app.UseCustomExceptionHandler();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) { }

    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = app.Environment.IsDevelopment() ? string.Empty : c.RoutePrefix;
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

        if (app.Environment.IsDevelopment())
        {
            c.EnablePersistAuthorization();
        }
    });

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers().RequireAuthorization();

    app.MapFallbackToFile("index.html");

    if (builder.Environment.IsDevelopment())
    {
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<VPMSDbContext>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred migrating the DB. {exceptionMessage}", ex.Message);
            }
        }
    }

}
app.Run();
