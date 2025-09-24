using FluentValidation;
using TaskTrackingSystem.Application.Tasks.Commands.Create;
namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
    public class AssignWorkItemToUserCommandValidator : AbstractValidator<AssignWorkItemToUserCommand>
    {
        public AssignWorkItemToUserCommandValidator()
        {
            RuleFor(x => x.WorkItemId)
                .NotEmpty().WithMessage("WorkItemId is required.");

            RuleFor(x => x.AssignedUserId)
                .NotEmpty().WithMessage("AssignedUserId is required");
        }
    }
}
