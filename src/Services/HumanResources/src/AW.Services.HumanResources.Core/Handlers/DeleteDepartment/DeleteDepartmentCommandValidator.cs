using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        private readonly IRepository<Entities.Department> _departmentRepository;

        public DeleteDepartmentCommandValidator(IRepository<Entities.Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;

            RuleFor(cmd => cmd.Name)
                .NotEmpty().WithMessage("Name is required")
                .MustAsync(Exist).WithMessage("Department does not exist");
        }

        private async Task<bool> Exist(string? name, CancellationToken cancellationToken)
        {
            return await _departmentRepository.AnyAsync(
                new GetDepartmentSpecification(name),
                cancellationToken
            );
        }
    }
}
