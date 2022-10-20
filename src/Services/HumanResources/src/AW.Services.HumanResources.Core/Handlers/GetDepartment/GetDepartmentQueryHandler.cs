using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartment
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, Department>
    {
        private readonly ILogger<GetDepartmentQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Department> _repository;

        public GetDepartmentQueryHandler(
            ILogger<GetDepartmentQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Department> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<Department> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting department from database");

            var spec = new GetDepartmentSpecification(
                request.Name
            );

            var department = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.DepartmentNull(department, request.Name, _logger);

            _logger.LogInformation("Returning department");
            return _mapper.Map<Department>(department);
        }
    }
}