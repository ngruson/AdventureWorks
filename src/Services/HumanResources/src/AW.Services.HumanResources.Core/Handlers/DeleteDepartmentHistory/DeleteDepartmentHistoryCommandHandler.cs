using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommandHandler : IRequestHandler<DeleteDepartmentHistoryCommand, Result>
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

        public async Task<Result> Handle(DeleteDepartmentHistoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employee from database");
                var spec = new GetEmployeeSpecification(request.Employee);
                var employee = await _employeeRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.EmployeeNull(employee, request.Employee, _logger);
                if (!result.IsSuccess)
                    return result;

                var departmentHistory = employee!.DepartmentHistory.SingleOrDefault(_ =>
                    _.ObjectId == request.ObjectId
                );
                result = Guard.Against.EmployeeDepartmentHistoryNull(
                    departmentHistory,
                    request.ObjectId,
                    _logger
                );
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Remove department history from employee");
                employee.DepartmentHistory.Remove(departmentHistory!);

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
