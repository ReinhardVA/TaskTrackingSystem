using FluentValidation;

namespace TaskTrackingSystem.Application.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() {

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required.");
               
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.Role)
                .IsInEnum().WithMessage("Invalid role specified.");
        }
    }
}
