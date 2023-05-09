﻿using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IRepository<Entities.Employee> _employeeRepository;

        public UpdateEmployeeCommandValidator(IRepository<Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(cmd => cmd.Employee!.Name!.FirstName)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.Name!.LastName)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.NationalIDNumber)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.LoginID)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.JobTitle)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.BirthDate)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.MaritalStatus)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.Gender)
                .NotEmpty();

            RuleFor(cmd => cmd.Employee!.HireDate)
                .NotEmpty();
        }

        //TODO
        //private async Task<bool> Exist(Guid objectId, CancellationToken cancellationToken)
        //{
        //    return await _departmentRepository.AnyAsync(
        //        new GetDepartmentSpecification(objectId),
        //        cancellationToken
        //    );
        //}
    }
}