using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, GetEmployeesResult?>
    {
        private readonly ILogger<GetEmployeesQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Employee> _repository;

        public GetEmployeesQueryHandler(
            ILogger<GetEmployeesQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Employee> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);
        
        public async Task<GetEmployeesResult?> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting customers from database");

            var spec = new GetEmployeesPaginatedSpecification(
                request.PageIndex,
                request.PageSize
            );
            var countSpec = new CountEmployeesSpecification();

            var employees = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.EmployeesNullOrEmpty(employees, _logger);

            _logger.LogInformation("Returning customers");
            return new GetEmployeesResult(
                employees: _mapper.Map<List<Employee>>(employees),
                totalEmployees: await _repository.CountAsync(countSpec, cancellationToken)
            );
        }
    }
}