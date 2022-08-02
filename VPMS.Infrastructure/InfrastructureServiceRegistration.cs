using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using VPMS.SharedKernel.Interfaces;
using VPMS.SharedKernel.Models;

namespace VPMS.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
