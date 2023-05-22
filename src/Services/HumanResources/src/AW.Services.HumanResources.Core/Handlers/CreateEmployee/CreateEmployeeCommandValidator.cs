using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IRepository<Entities.Employee> _employeeRepository;

        public CreateEmployeeCommandValidator(IRepository<Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(cmd => cmd.Employee!.Name)
                .NotNull();

            RuleFor(cmd => cmd.Employee.Name!.FirstName)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.Name!.LastName)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.LoginID)
                .NotEmpty()
                .MustAsync(NotExists)
                .WithMessage("'Employee Login ID' already exists");

            RuleFor(cmd => cmd.Employee.NationalIDNumber)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.JobTitle)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.BirthDate)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.MaritalStatus)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.Gender)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee.HireDate)
                .NotEmpty();
        }

        private async Task<bool> NotExists(string? loginID, CancellationToken cancellationToken)
        {
            return !await _employeeRepository.AnyAsync(
                new GetEmployeeByLoginIDSpecification(loginID!),
                cancellationToken
            );
        }
    }
}
