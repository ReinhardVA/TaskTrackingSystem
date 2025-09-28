using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Infrastructure.BackgroundJobs;
using TaskTrackingSystem.Infrastructure.Services;

namespace TaskTrackingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEmailSender, EmailSenderService>();
            service.AddHostedService<DueDateCheckerBackgroundJob>();
            return service;
        }
    }
}
