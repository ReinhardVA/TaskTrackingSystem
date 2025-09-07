namespace TaskTrackingSystem.Domain.Entities
{
    public class Assessment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
