using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartmentHistory
{
    public class UpdateDepartmentHistoryCommandHandler : IRequestHandler<UpdateDepartmentHistoryCommand, Result>
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

        public async Task<Result> Handle(UpdateDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employee from database");
                var spec = new GetEmployeeSpecification(request.Employee);
                var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.Employee, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Getting department from database");
                var departmentSpec = new GetDepartmentSpecification(request.Department);
                var department = await _departmentRepository.SingleOrDefaultAsync(departmentSpec, cancellationToken);
                result = Guard.Against.DepartmentNull(department, request.Department, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Getting shift from database");
                var shiftSpec = new GetShiftSpecification(request.Shift);
                var shift = await _shiftRepository.SingleOrDefaultAsync(shiftSpec, cancellationToken);
                result = Guard.Against.ShiftNull(shift, request.Shift, _logger);
                if (!result.IsSuccess)
                    return result;

                var departmentHistory = employee!.DepartmentHistory.SingleOrDefault(_ =>
                    _.ObjectId == request.ObjectId
                );
                Guard.Against.EmployeeDepartmentHistoryNull(departmentHistory,
                    request.ObjectId,
                    _logger
                );

                _logger.LogInformation("Updating department history with new values");
                _mapper.Map(request, departmentHistory);
                departmentHistory!.Department = department;
                departmentHistory.Shift = shift;

                _logger.LogInformation("Saving employee to database");
                await _employeeRepository.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
