using FluentValidation;

namespace TaskTrackingSystem.Application.WorkItems.Commands.Create
{
    public class CreateWorkItemCommandValidator : AbstractValidator<CreateWorkItemCommand>
    {
        public CreateWorkItemCommandValidator()
        {
            RuleFor(w => w.Title)
                .MinimumLength(5).WithMessage("Title must be at least 5 characters long.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(w => w.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(w => w.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("DueDate must be a future date.");
        }
    }
}
