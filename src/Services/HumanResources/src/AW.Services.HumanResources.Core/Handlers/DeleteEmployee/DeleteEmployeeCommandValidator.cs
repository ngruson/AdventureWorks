using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        private readonly IRepository<Entities.Employee> _employeeRepository;

        public DeleteEmployeeCommandValidator(IRepository<Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(cmd => cmd.ObjectId)
                .NotEmpty().WithMessage("ObjectId is required")
                .MustAsync(Exist).WithMessage("Employee does not exist");
        }

        private async Task<bool> Exist(Guid objectId, CancellationToken cancellationToken)
        {
            return await _employeeRepository.AnyAsync(
                new GetEmployeeSpecification(objectId),
                cancellationToken
            );
        }
    }
}
