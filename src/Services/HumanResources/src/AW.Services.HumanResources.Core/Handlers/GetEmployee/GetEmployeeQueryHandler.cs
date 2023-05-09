using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Result<Employee>>
    {
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Employee> _repository;

        public GetEmployeeQueryHandler(
            ILogger<GetEmployeeQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Employee> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Result<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employee from database");

                var spec = new GetEmployeeSpecification(
                    request.LoginID
                );

                var employee = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.LoginID!, _logger);
                if (!result.IsSuccess)
                    return result;

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
