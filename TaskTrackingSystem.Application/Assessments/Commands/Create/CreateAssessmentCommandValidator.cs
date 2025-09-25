using FluentValidation;

namespace TaskTrackingSystem.Application.Assessments.Commands.Create
{
    public class CreateAssessmentCommandValidator : AbstractValidator<CreateAssessmentCommand>
    {
        public CreateAssessmentCommandValidator()
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("Score must be between 0 and 100");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

        }

    }
}
