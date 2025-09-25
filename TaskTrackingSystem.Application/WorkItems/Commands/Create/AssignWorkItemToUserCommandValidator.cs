using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskTrackingSystem.Application.Common.Interfaces;
namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
    public class AssignWorkItemToUserCommandValidator : AbstractValidator<AssignWorkItemToUserCommand>
    {
        private readonly IAppDbContext _context;
        public AssignWorkItemToUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.WorkItemId)
                .NotEmpty().WithMessage("WorkItemId is required.")
                .NotEqual(Guid.Empty).WithMessage("WorkItemId cannot be empty Guid")
                .MustAsync(WorkItemExists).WithMessage("WorkItem does not exist.");

            RuleFor(x => x.AssignedUserId)
                 .NotEmpty().WithMessage("AssignedUserId is required.")
                 .NotEqual(Guid.Empty).WithMessage("AssignedUserId cannot be empty Guid")
                 .MustAsync(UserExists).WithMessage("Assigned user does not exist.");
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken token)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId, token);

        }

        private async Task<bool> WorkItemExists(Guid userId, CancellationToken token)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId, token);

        }
    }
}
