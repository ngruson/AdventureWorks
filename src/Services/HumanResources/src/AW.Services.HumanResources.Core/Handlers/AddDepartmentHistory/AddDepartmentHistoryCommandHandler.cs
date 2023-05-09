using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.AddDepartmentHistory
{
    public class AddDepartmentHistoryCommandHandler : IRequestHandler<AddDepartmentHistoryCommand, Result>
    {
        private readonly ILogger<AddDepartmentHistoryCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IRepository<Entities.Employee> _employeeRepository;
        private readonly IRepository<Entities.Shift> _shiftRepository;
        private readonly IMapper _mapper;

        public AddDepartmentHistoryCommandHandler(
            ILogger<AddDepartmentHistoryCommandHandler> logger,
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

        public async Task<Result> Handle(AddDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employee from database");
                var spec = new GetEmployeeSpecification(request.LoginID);
                var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.LoginID!, _logger);
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

                foreach (var item in employee!.DepartmentHistory)
                {
                    if (item.EndDate == null)
                        item.EndDate = DateTime.Today;
                }

                employee.DepartmentHistory.Add(new Entities.EmployeeDepartmentHistory
                {
                    Department = department,
                    Shift = shift,
                    StartDate = request.StartDate
                });

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
