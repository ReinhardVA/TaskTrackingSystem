using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Update
{
    public class UpdateWorkItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? AssignedUserId { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateWorkItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.WorkItems.FindAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Work item not found");
            }
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.AssignedUserId = request.AssignedUserId;
            entity.Status = request.Status;
            entity.DueDate = request.DueDate;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
