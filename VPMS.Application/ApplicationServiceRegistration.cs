using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using VPMS.Application.Contracts.Security;
using VPMS.Application.Contracts.ToDoItem;
using VPMS.Application.Security;
using VPMS.Application.TodoItem;

namespace VPMS.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.TryAddScoped<ISecurityService, SecurityService>();
        services.TryAddScoped<ITodoItemService, TodoItemService>();
        services.TryAddScoped<ITokenBuilder, TokenBuilder>();
        services.TryAddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
