using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.Application.Contracts.Persistence;
using POC.Persistence.Repositories;

namespace POC.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<POCDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("POCConnectionString"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        //services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
