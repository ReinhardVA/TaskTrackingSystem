namespace TaskTrackingSystem.Domain.Entities
{
    public class Assessment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
