using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Department>
    {
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(
            ILogger<UpdateDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Department> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting department from database");
            var spec = new GetDepartmentSpecification(request.Key);
            var department = await _departmentRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.DepartmentNull(department, request.Key, _logger);

            _logger.LogInformation("Updating department");
            _mapper.Map(request.Department, department);

            _logger.LogInformation("Saving department to database");
            await _departmentRepository.UpdateAsync(department!, cancellationToken);

            _logger.LogInformation("Returning department");
            return _mapper.Map<Department>(department);
        }
    }
}
