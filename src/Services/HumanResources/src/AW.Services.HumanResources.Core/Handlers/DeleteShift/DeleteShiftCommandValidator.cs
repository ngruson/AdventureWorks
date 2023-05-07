using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.DeleteShift
{
    public class DeleteShiftCommandValidator : AbstractValidator<DeleteShiftCommand>
    {
        private readonly IRepository<Entities.Shift> _shiftRepository;

        public DeleteShiftCommandValidator(IRepository<Entities.Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;

            RuleFor(cmd => cmd.ObjectId)
                .NotEmpty().WithMessage("ObjectId is required")
                .MustAsync(Exist).WithMessage("Shift does not exist");
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
