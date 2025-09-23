using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Assessment> Assessments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Assessments (one-to-many)
            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assessments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(w => w.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
