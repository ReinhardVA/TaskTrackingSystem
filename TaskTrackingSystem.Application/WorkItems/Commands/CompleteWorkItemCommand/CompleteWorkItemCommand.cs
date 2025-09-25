using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Attributes;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.WorkItems.Commands.CompleteWorkItemCommand { 

    [Authorize(Role.Admin)]
    public class CompleteWorkItemCommand : IRequest
    {
        public Guid WorkItemId { get; set; }
    }

    public class CompleteWorkItemCommandHandler : IRequestHandler<CompleteWorkItemCommand>
    {
        readonly private IAppDbContext _context;
        public CompleteWorkItemCommandHandler(IAppDbContext context) => _context = context;

        public async Task Handle(CompleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _context.WorkItems.FirstOrDefaultAsync(w => w.Id == request.WorkItemId, cancellationToken);

            if (workItem is null)
            {
                throw new Exception("Work item not found");
            }

            if(workItem.Status == Status.Done)
            {
                throw new Exception("Work item is already completed");
            }
            if (!workItem.AssignedUserId.HasValue)
            {
                throw new Exception("WorkItem has no assigned user");
            }

            workItem.Status = Status.Done;

            var assessment = new Assessment
            {
                Id = Guid.NewGuid(),
                UserId = workItem.AssignedUserId.Value,
                Score = Random.Shared.Next(1,10),
                CreatedAt = DateTime.UtcNow,
            };

            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
