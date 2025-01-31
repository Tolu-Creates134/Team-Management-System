using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using TeamManagementSystem.Application.Common.Behaviours;

namespace TeamManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services) {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TokenValidationBehaviour<,>));

        return services;
    }

}
