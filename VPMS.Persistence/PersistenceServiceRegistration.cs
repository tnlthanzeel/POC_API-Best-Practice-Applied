using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Persistence.Repositories.Security;
using VPMS.Persistence.Repositories.TodoRepository;

namespace VPMS.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VPMSDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("VPMSSqlDbConnection"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


        services.TryAddScoped<ITodoItemRepository, TodoItemRepository>();
        services.TryAddScoped<IUserSecurityRespository, UserSecurityRespository>();

        return services;
    }
}
