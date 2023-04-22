using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(
            ILogger<UpdateEmployeeCommandHandler> logger,
            IRepository<Entities.Employee> employeeRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting employee from database");
            var spec = new GetEmployeeSpecification(request.Key);
            var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.EmployeeNull(employee, request.Key, _logger);

            _logger.LogInformation("Updating employee");
            _mapper.Map(request.Employee, employee);

            _logger.LogInformation("Saving employee to database");
            await _employeeRepository.UpdateAsync(employee!, cancellationToken);

            _logger.LogInformation("Returning employee");
            return _mapper.Map<Employee>(employee);
        }
    }
}
