using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.Application.Common.Interfaces;
using TaskTrackingSystem.Domain.Enums;

namespace TaskTrackingSystem.Application.WorkItems.Commands.CompleteWorkItemCommand
{
    public class CompleteWorkItemCommandValidator : AbstractValidator<CompleteWorkItemCommand>
    {
        public CompleteWorkItemCommandValidator(IAppDbContext context)
        {
            RuleFor(w => w.WorkItemId)
                .NotEmpty().WithMessage("WorkItemId is required.")
                .MustAsync(async (id, ct) =>
                {
                    var status = await context.WorkItems
                        .Where(x => x.Id == id)
                        .Select(x => (Status?)x.Status)
                        .FirstOrDefaultAsync(ct);

                    if (status is null) return true;

                    return status.Value != Status.Done;
                })
                .WithMessage("Work item is already completed.");
        }
    }
}
