using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        Role? Role { get; }
    }
}
