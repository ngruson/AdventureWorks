using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        private readonly IRepository<Entities.Department> _departmentRepository;

        public CreateDepartmentCommandValidator(IRepository<Entities.Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;

            RuleFor(cmd => cmd.Department!.Name)
                .NotEmpty().WithMessage("Name is required")
                .MustAsync(NotExists).WithMessage("Department already exists");

            RuleFor(cmd => cmd.Department!.GroupName)
                .NotEmpty().WithMessage("Group name is required");
        }

        private async Task<bool> NotExists(string? name, CancellationToken cancellationToken)
        {
            return !await _departmentRepository.AnyAsync(
                new GetDepartmentByNameSpecification(name!),
                cancellationToken
            );
        }
    }
}
