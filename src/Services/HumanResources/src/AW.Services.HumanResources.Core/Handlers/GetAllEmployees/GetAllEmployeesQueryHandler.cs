using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly ILogger<GetAllEmployeesQueryHandler> _logger;
        private readonly IRepository<Entities.Employee> _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(
            ILogger<GetAllEmployeesQueryHandler> logger,
            IRepository<Entities.Employee> repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employees for request {@Request}", request);

            var spec = new GetAllEmployeesSpecification();

            var employees = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.EmployeesNullOrEmpty(employees, _logger);

            _logger.LogInformation("Returning {Count} employees", employees.Count);

            return _mapper.Map<List<Employee>>(employees);
        }
    }
}