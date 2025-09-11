using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Application.Common.Exceptions;
using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Application.WorkItems.Queries.GetWorkItemByIdQuery
{
    public class GetWorkItemByIdQuery : IRequest<WorkItemByIdVm>
    {
        public Guid Id { get; set; }
    }

    public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, WorkItemByIdVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkItemByIdQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WorkItemByIdVm> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
        {
            var workItem = await _context.WorkItems
                .FirstOrDefaultAsync(w => w.Id == request.Id);
            if (workItem == null) {
                throw new NotFoundException(nameof(workItem), request.Id);
            }

            return _mapper.Map<WorkItemByIdVm>(workItem);
        }
    }
}
