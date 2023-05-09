using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        private readonly IRepository<Entities.Department> _departmentRepository;

        public UpdateDepartmentCommandValidator(IRepository<Entities.Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;

            RuleFor(cmd => cmd.Department.ObjectId)
                .NotEmpty().WithMessage("ObjectId is required")
                .MustAsync(Exist).WithMessage("Department does not exist");

            RuleFor(cmd => cmd.Department.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(cmd => cmd.Department.GroupName)
                .NotEmpty().WithMessage("Group name is required");
        }

        private async Task<bool> Exist(Guid objectId, CancellationToken cancellationToken)
        {
            return await _departmentRepository.AnyAsync(
                new GetDepartmentSpecification(objectId),
                cancellationToken
            );
        }
    }
}
