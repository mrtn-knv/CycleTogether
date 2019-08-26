using System;
using FluentValidation;

namespace CycleTogether.Validation
{
    public class Route : AbstractValidator<WebModels.Route>
    {
        public Route()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Route's title cannot be empty.");

            RuleFor(r => r.Info)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("The field cannot be empty.")
                .MinimumLength(50)
                .WithMessage("Information must contain at least 50 characters.");

            RuleFor(r => r.StartPoint)
                .NotEmpty()
                .WithMessage("Start point is required.");

            RuleFor(r => r.Destination)
                .NotEmpty()
                .WithMessage("Destination is required.");

            RuleFor(r => r.StartTime)
                .Must(IsValidDate)
                .WithMessage("The start time must be at least one day after creation of the trip.");

            RuleFor(r => r.Endurance)
                .IsInEnum();

            RuleFor(r => r.Terrain)
                .IsInEnum();

            RuleFor(r => r.Type)
                .IsInEnum();
        }

        private bool IsValidDate(DateTime startTime)
        {
            if (DateTime.Compare(startTime, DateTime.Now) <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
