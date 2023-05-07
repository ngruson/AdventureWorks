using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.UpdateShift
{
    public class UpdateShiftCommandValidator : AbstractValidator<UpdateShiftCommand>
    {
        private readonly IRepository<Entities.Shift> _shiftRepository;

        public UpdateShiftCommandValidator(IRepository<Entities.Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;

            RuleFor(cmd => cmd.Shift)
                .NotNull().WithMessage("Shift is required");

            RuleFor(cmd => cmd.Shift!.ObjectId)
                .NotEmpty().WithMessage("ObjectId is required")
                .MustAsync(Exist).WithMessage("Shift does not exist");

            RuleFor(cmd => cmd.Shift!.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(cmd => cmd.Shift!.StartTime)
                .NotEmpty().WithMessage("Start time is required");

            RuleFor(cmd => cmd.Shift!.EndTime)
                .NotEmpty().WithMessage("End time is required");
        }

        private async Task<bool> Exist(Guid objectId, CancellationToken cancellationToken)
        {
            return await _shiftRepository.AnyAsync(
                new GetShiftSpecification(objectId),
                cancellationToken
            );
        }
    }
}
