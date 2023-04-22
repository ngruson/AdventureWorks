using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommandHandler : IRequestHandler<UpdateDepartmentHistoryCommand>
    {
        private readonly ILogger<UpdateDepartmentHistoryCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;

        public UpdateDepartmentHistoryCommandHandler(
            ILogger<UpdateDepartmentHistoryCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IRepository<Entities.Employee> employeeRepository,
            IRepository<Entities.Shift> shiftRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting employee from database");
            var spec = new GetEmployeeSpecification(request.LoginID);
            var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.EmployeeNull(employee, request.LoginID!, _logger);

            _logger.LogInformation("Getting department from database");
            var departmentSpec = new GetDepartmentSpecification(request.DepartmentName);
            var department = await _departmentRepository.SingleOrDefaultAsync(departmentSpec, cancellationToken);
            Guard.Against.DepartmentNull(department, request.DepartmentName!, _logger);

            _logger.LogInformation("Getting shift from database");
            var shiftSpec = new GetShiftSpecification(request.ShiftName);
            var shift = await _shiftRepository.SingleOrDefaultAsync(shiftSpec, cancellationToken);
            Guard.Against.ShiftNull(shift, request.ShiftName!, _logger);

            var departmentHistory = employee!.DepartmentHistory.SingleOrDefault(_ =>
                _.Department!.Name == request.DepartmentName &&
                _.Shift!.Name == request.ShiftName &&
                _.StartDate == request.StartDate
            );
            Guard.Against.EmployeeDepartmentHistoryNull(departmentHistory,
                request.LoginID!,
                request.DepartmentName,
                request.ShiftName,
                request.StartDate,
                _logger
            );

            _logger.LogInformation("Updating department history with new values");
            _mapper.Map(request, departmentHistory);

            _logger.LogInformation("Saving employee to database");
            await _employeeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
