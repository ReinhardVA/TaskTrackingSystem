using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.Tasks.Commands.Create
{
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
            var workItem = await _context.WorkItems.FindAsync( [request.WorkItemId] , cancellationToken);

            if(workItem != null)
            {
                throw new NotFoundException(nameof(workItem), request.WorkItemId);
            }

            workItem.AssignedUserId = request.AssignedUserId;
            workItem.Status = Status.Pending;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
