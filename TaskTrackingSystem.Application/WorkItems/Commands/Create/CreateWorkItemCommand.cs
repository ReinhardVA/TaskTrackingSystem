using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
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
