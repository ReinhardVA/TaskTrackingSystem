
using MediatR;
using TaskTrackingSystem.Application.Common.Attributes;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Application.Common.Behaivors
{
    public class AuthorizationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        public AuthorizationBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            IEnumerable<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if(_currentUserService.Role is null)
                {
                    throw new ForbiddenAccessException("User is not authorized");
                }
                
                foreach(var attribute in authorizeAttributes)
                {
                    if(_currentUserService.Role != attribute.Role)
                    {
                       throw new ForbiddenAccessException($"Role `{attribute.Role}` required");
                    }
                }
            }
            return await next();
        }
    }
}
