using FluentValidation;

namespace Baldris.Application.Calendar.Groomings.Commands.Update
{
    public class UpdateGroomingCommandValidator : AbstractValidator<UpdateGroomingCommand>
    {
        public UpdateGroomingCommandValidator()
        {
            RuleFor(x => x.Subject).NotEmpty()
                .WithMessage("Subject is required.");
            RuleFor(x => x.Start)
                .Must((x, y) => !x.End.HasValue || !y.HasValue || y.Value <= x.End.Value)
                .WithMessage("Start time must come before the end time.");
            RuleFor(x => x.End)
                .Must((x, y) => !x.Start.HasValue || !y.HasValue || y.Value >= x.Start.Value)
                .WithMessage("End time must come after the start time.");
        }
    }
}