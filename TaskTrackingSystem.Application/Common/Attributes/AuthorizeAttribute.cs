
namespace TaskTrackingSystem.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        public string Role { get; }
        public AuthorizeAttribute(string role)
        {
            Role = role;
        }
    }
}
