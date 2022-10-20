using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<Department>>
    {
        private readonly ILogger<GetDepartmentsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Entities.Department> _repository;

        public GetDepartmentsQueryHandler(
            ILogger<GetDepartmentsQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Department> repository
        ) => (_logger, _mapper, _repository) = (logger, mapper, repository);

        public async Task<List<Department>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");
            _logger.LogInformation("Getting departments from database");

            var departments = await _repository.ListAsync(cancellationToken);
            Guard.Against.DepartmentsNull(departments, _logger);

            _logger.LogInformation("Returning departments");
            return _mapper.Map<List<Department>>(departments);
        }
    }
}