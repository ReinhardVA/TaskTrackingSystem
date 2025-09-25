using MediatR;
using TaskTrackingSystem.Application.Common.Attributes;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
    [Authorize(Role.Admin)]
    public class AssignWorkItemToUserCommand : IRequest<Unit>
    {
        public Guid WorkItemId { get; set; }
        public Guid AssignedUserId { get; set; }
    }
    public class AssignWorkItemToUserCommandHandler : IRequestHandler<AssignWorkItemToUserCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public AssignWorkItemToUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AssignWorkItemToUserCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _context.WorkItems.FindAsync([request.WorkItemId], cancellationToken);

            if (workItem == null)
            {
                throw new NotFoundException(nameof(workItem), request.WorkItemId);
            }

            workItem.AssignedUserId = request.AssignedUserId;
            workItem.Status = Status.Pending;
            workItem.User = _context.Users.Find(request.AssignedUserId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
