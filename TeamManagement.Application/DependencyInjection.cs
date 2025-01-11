using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace TeamManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services) {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}