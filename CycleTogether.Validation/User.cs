using FluentValidation;

namespace CycleTogether.Validation
{
    public class User : AbstractValidator<WebModels.User>
    {
        public User()
        {
            RuleFor(u => u.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Email address must be valid");

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(5)
                .WithMessage("Password is too short");

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(u => u.Endurance)
                .IsInEnum();

            RuleFor(u => u.Terrain)
                .IsInEnum();

            RuleFor(u => u.Type)
                .IsInEnum();
        }
    }
}
