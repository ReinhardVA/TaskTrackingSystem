using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackingSystem.Application.Common.Behaivors;
using TaskTrackingSystem.Application.WorkItems.Commands.Create;

namespace TaskTrackingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddValidatorsFromAssembly(typeof(AssignWorkItemToUserCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }
    }
}
