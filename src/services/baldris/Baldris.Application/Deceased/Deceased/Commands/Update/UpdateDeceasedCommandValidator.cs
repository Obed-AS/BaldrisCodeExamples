using FluentValidation;

namespace Baldris.Application.Deceased.Deceased.Commands.Update
{
    public class UpdateDeceasedCommandValidator : AbstractValidator<UpdateDeceasedCommand>
    {
        public UpdateDeceasedCommandValidator()
        {
            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Last name is required.");
            RuleFor(x => x.DateOfBirth)
                .Must((x, y) => !x.DateOfDeath.HasValue || !y.HasValue || y.Value <= x.DateOfDeath.Value)
                .WithMessage("Date of birth must come before the date of death.");
            RuleFor(x => x.DateOfDeath)
                .Must((x, y) => !x.DateOfBirth.HasValue || !y.HasValue || y.Value >= x.DateOfBirth.Value)
                .WithMessage("Date of death must come after the date of birth.");
        }
    }
}