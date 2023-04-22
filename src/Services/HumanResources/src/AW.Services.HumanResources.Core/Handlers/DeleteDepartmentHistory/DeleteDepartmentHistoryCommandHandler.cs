using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommandHandler : IRequestHandler<DeleteDepartmentHistoryCommand>
    {
        private readonly ILogger<DeleteDepartmentHistoryCommandHandler> _logger;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public DeleteDepartmentHistoryCommandHandler(
            ILogger<DeleteDepartmentHistoryCommandHandler> logger,
            IRepository<Entities.Employee> employeeRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employee from database");
            var spec = new GetEmployeeSpecification(request.LoginID);
            var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.EmployeeNull(employee, request.LoginID!, _logger);

            var departmentHistory = employee!.DepartmentHistory.SingleOrDefault(_ =>
                _.Department!.Name == request.DepartmentName &&
                _.Shift!.Name == request.ShiftName &&
                _.StartDate == request.StartDate
            );
            Guard.Against.EmployeeDepartmentHistoryNull(
                departmentHistory,
                request.LoginID!,
                request.DepartmentName, 
                request.ShiftName, 
                request.StartDate,
                _logger
            );

            _logger.LogInformation("Remove department history from employee");
            employee.DepartmentHistory.Remove(departmentHistory!);

            _logger.LogInformation("Saving employee to database");
            await _employeeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
