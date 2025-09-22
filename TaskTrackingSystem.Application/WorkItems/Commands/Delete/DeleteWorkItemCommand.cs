using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Delete
{
    public class DeleteWorkItemCommand : IRequest
    {
        public Guid Id { get; set; }

    }
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand>
    {
        private readonly IAppDbContext _context;
        public DeleteWorkItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.WorkItems.FindAsync(request.Id);
            if(entity == null)
            {
                throw new Exception("Work item not found");
            }
            _context.WorkItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
