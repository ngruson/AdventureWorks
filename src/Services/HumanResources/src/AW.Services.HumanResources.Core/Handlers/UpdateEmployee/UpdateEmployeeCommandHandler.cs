using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<Employee>>
    {
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateEmployeeCommand> _validator;

        public UpdateEmployeeCommandHandler(
            ILogger<UpdateEmployeeCommandHandler> logger,
            IRepository<Entities.Employee> employeeRepository,
            IMapper mapper,
            IValidator<UpdateEmployeeCommand> validator
        )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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
                var spec = new GetEmployeeSpecification(request.Key);
                var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.Key, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Updating employee");
                _mapper.Map(request.Employee, employee);

                _logger.LogInformation("Saving employee to database");
                await _employeeRepository.UpdateAsync(employee!, cancellationToken);

                _logger.LogInformation("Returning employee");
                return Result.Success(
                    _mapper.Map<Employee>(employee)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
