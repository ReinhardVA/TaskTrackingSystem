namespace TaskTrackingSystem.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task DeleteUserAsync(Guid userId);
    }
}
