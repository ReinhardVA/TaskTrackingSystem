using MediatR;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Entities;

namespace TaskTrackingSystem.Application.Assessments.Commands.Create
{
    public class CreateAssessmentCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }

    public class CreateAssessmentCommandHandler : IRequestHandler<CreateAssessmentCommand, Guid> {

        private readonly IAppDbContext _context;

        public CreateAssessmentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Assessment
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Score = request.Score,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Assessments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
