using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId => "test-user";

        public string? Role => "Admin";
    }
}
