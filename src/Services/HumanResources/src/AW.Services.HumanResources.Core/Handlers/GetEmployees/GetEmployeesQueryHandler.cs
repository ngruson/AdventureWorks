using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
    {
        private readonly ILogger<GetEmployeesQueryHandler> _logger;
        private readonly IRepository<Entities.Employee> _repository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(
            ILogger<GetEmployeesQueryHandler> logger,
            IRepository<Entities.Employee> repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employees for request {@Request}", request);

            var spec = new GetEmployeesSpecification();

            var employees = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.EmployeesNullOrEmpty(employees, _logger);

            _logger.LogInformation("Returning {Count} employees", employees.Count);

            return _mapper.Map<List<Employee>>(employees);
        }
    }
}
