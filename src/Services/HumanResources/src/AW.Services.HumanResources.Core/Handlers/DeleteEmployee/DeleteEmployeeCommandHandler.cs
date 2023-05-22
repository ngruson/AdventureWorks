using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IValidator<DeleteEmployeeCommand> _validator;

        public DeleteEmployeeCommandHandler(
            ILogger<DeleteEmployeeCommandHandler> logger,
            IRepository<Entities.Employee> employeeRepository,
            IValidator<DeleteEmployeeCommand> validator
        )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Getting employee from database");
                var spec = new GetEmployeeSpecification(request.ObjectId);
                var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Deleting employee from database");
                await _employeeRepository.DeleteAsync(employee!, cancellationToken);

                _logger.LogInformation("Deleted employee from database");

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
