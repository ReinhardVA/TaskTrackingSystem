using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<WorkItem> WorkItems { get; set; } // Tasks
        DbSet<Assessment> Assessments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
