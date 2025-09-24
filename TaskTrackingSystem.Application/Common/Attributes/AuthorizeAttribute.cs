
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        public Role Role { get; }
        public AuthorizeAttribute(Role role)
        {
            Role = role;
        }
    }
}
