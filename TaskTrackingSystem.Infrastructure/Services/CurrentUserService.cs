using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId => "test-user";

        public Role? Role => Domain.Enums.Role.Member;
    }
}
