using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Result<List<Employee>>>
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

        public async Task<Result<List<Employee>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employees for request {@Request}", request);

                var spec = new GetEmployeesSpecification();

                var employees = await _repository.ListAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeesNullOrEmpty(employees, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Returning {Count} employees", employees.Count);

                return Result.Success(
                    _mapper.Map<List<Employee>>(employees)
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
