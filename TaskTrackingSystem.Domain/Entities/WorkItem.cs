using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Domain.Entities
{
    public class WorkItem
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public Guid? AssignedUserId { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }
        public User? User { get; set; }
    }
}
