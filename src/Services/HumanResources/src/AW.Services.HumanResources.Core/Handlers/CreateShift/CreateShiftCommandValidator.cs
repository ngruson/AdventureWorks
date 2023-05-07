using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class CreateShiftCommandValidator : AbstractValidator<CreateShiftCommand>
    {
        public CreateShiftCommandValidator()
        {
            RuleFor(cmd => cmd.Shift)
                .NotNull().WithMessage("Shift is required");

            RuleFor(cmd => cmd.Shift!.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(cmd => cmd.Shift!.StartTime)
                .NotEmpty().WithMessage("Start time is required");

            RuleFor(cmd => cmd.Shift!.EndTime)
                .NotEmpty().WithMessage("End time is required");
        }
    }
}
