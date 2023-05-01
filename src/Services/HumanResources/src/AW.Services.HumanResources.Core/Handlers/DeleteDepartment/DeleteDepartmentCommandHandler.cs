using Ardalis.GuardClauses;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;

        public DeleteDepartmentCommandHandler(
            ILogger<DeleteDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting department from database");
            var spec = new GetDepartmentSpecification(request.Name);
            var department = await _departmentRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.DepartmentNull(department, request.Name!, _logger);

            _logger.LogInformation("Deleting department from database");
            await _departmentRepository.DeleteAsync(department!, cancellationToken);

            _logger.LogInformation("Deleted department from database");
        }
    }
}
