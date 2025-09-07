using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Role Role { get; set; }

        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();,
        public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
