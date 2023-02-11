using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Employee> _repository;

        public GetEmployeeQueryHandler(
            ILogger<GetEmployeeQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Employee> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting employee from database");

            var spec = new GetEmployeeSpecification(
                request.LoginID!
            );

            var employee = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.EmployeeNull(employee, request.LoginID, _logger);

            _logger.LogInformation("Returning employee");
            return _mapper.Map<Employee>(employee);
        }
    }
}