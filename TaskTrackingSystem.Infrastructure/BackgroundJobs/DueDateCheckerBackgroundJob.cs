using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;
using TaskTrackingSystem.Infrastructure.Services;

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

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                    var expiredTasks = await db.WorkItems
                        .Where(w => w.DueDate < DateTime.UtcNow && w.Status != Status.Done && w.AssignedUserId != null)
                        .Select( w => new
                        {
                            w.Id,
                            w.Title,
                            w.DueDate,
                            w.AssignedUserId,
                            UserEmail = w.User != null ? w.User.Email : null
                        })
                        .ToListAsync(stoppingToken);

                    if (expiredTasks.Count == 0) continue;

                    foreach (var task in expiredTasks)
                    {
                        if(string.IsNullOrWhiteSpace(task.UserEmail))
                        {
                            _logger.LogWarning("Task with ID {TaskId} is overdue but has no assigned user email.", task.Id);
                            continue;
                        }
                        await emailSender.SendEmailAsync(
                            task.UserEmail, // Mail adresi buraya 
                            "Task Overdue Notification",
                            $"The task '{task.Title}' was due on {task.DueDate} and is now overdue. Please take the necessary actions."
                        );
                        _logger.LogWarning("Task with ID {TaskId} is overdue. Due date was {DueDate}.", task.Id, task.DueDate);
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "An error occurred while checking for overdue tasks.");
                }
            }
        }       
    }
}
