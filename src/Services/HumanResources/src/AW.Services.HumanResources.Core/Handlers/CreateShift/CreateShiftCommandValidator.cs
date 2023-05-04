using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.CreateShift
{
    public class CreateShiftCommandValidator : AbstractValidator<CreateShiftCommand>
    {
        private readonly IRepository<Entities.Shift> _shiftRepository;

        public CreateShiftCommandValidator(IRepository<Entities.Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;

            RuleFor(cmd => cmd.Shift)
                .NotNull().WithMessage("Shift is required");

            RuleFor(cmd => cmd.Shift!.Name)
                .NotEmpty().WithMessage("Name is required")
                .MustAsync(NotExists).WithMessage("Shift already exists");

            RuleFor(cmd => cmd.Shift!.StartTime)
                .NotEmpty().WithMessage("Start time is required");

            RuleFor(cmd => cmd.Shift!.EndTime)
                .NotEmpty().WithMessage("End time is required");
        }

        private async Task<bool> NotExists(string? name, CancellationToken cancellationToken)
        {
            return !await _shiftRepository.AnyAsync(
                new GetShiftSpecification(name),
                cancellationToken
            );
        }
    }
}
