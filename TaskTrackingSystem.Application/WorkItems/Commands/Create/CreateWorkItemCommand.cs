using MediatR;
using TaskTrackingSystem.Application.Common.Attributes;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
    [Authorize(Role.Admin)]
    public class CreateWorkItemCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

    }

    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateWorkItemCommandHandler(IAppDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = new WorkItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate.ToUniversalTime(),
            };

            _context.WorkItems.Add(workItem);
            await _context.SaveChangesAsync(cancellationToken);
            return workItem.Id;

        }
    }
}
