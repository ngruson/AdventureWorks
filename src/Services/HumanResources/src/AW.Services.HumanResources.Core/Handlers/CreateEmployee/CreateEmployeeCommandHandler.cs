using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<CreatedEmployee>>
    {
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        public CreateEmployeeCommandHandler(
            ILogger<CreateEmployeeCommandHandler> logger,
            IRepository<Entities.Employee> employeeRepository,
            IMapper mapper,
            IValidator<CreateEmployeeCommand> validator
        )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<CreatedEmployee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Saving new employee to database");
                var employee = _mapper.Map<Entities.Employee>(request.Employee);
                employee = await _employeeRepository.AddAsync(employee, cancellationToken);

                _logger.LogInformation("Returning employee");
                return Result.Success(
                    _mapper.Map<CreatedEmployee>(employee)
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
