using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Assessments.Queries
{
    public class GetAssessmentByUserQuery : IRequest<List<Assessment>>
    {
        public Guid UserId { get; set; }

    }

    public class GetAssessmentByQueryHandler : IRequestHandler<GetAssessmentByUserQuery, List<Assessment>>
    {
        private readonly IAppDbContext _context;

        public GetAssessmentByQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Assessment>> Handle(GetAssessmentByUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Assessments
                .Where(a => a.UserId == request.UserId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
