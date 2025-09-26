using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Infrastructure.BackgroundJobs
{
    public class DueDateCheckerBackgroundJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DueDateCheckerBackgroundJob> _logger;
        public DueDateCheckerBackgroundJob(IServiceProvider serviceProvider, ILogger<DueDateCheckerBackgroundJob> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(TimeSpan.FromMinutes(1));

            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

                    var expiredTasks = db.WorkItems
                        .Where(w => w.DueDate < DateTime.UtcNow && w.Status != Status.Done)
                        .ToList();
                    
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                    foreach (var task in expiredTasks)
                    {
                        await emailSender.SendEmailAsync(
                            "eren@example.com", // Mail adresi buraya 
                            "Task Overdue Notification", 
                            $"The task '{task.Title}' was due on {task.DueDate} and is now overdue. Please take the necessary actions."
                        );
                        _logger.LogWarning("Task with ID {TaskId} is overdue. Due date was {DueDate}.", task.Id, task.DueDate);
                    }
                } catch(Exception ex)
                {
                     
                    _logger.LogError(ex, "An error occurred while checking for overdue tasks.");
                }
            }
        }
    }
}
