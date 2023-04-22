using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;
using AW.UI.Web.SharedKernel.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployee;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetJobTitles;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EmployeeService(
            ILogger<ProductService> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<EmployeeViewModel>> GetEmployees()
        {
            _logger.LogInformation("Getting employees");
            var response = await _mediator.Send(new GetEmployeesQuery());

            var vm = _mapper.Map<List<EmployeeViewModel>>(response);

            _logger.LogInformation("Returning {ViewModel}", vm);
            return vm;
        }

        public async Task<EmployeeDetailViewModel> GetDetail(string loginID)
        {
            var employee = await GetEmployee(loginID);

            return new EmployeeDetailViewModel
            {
                Employee = _mapper.Map<EmployeeViewModel>(employee)
            };
        }

        private async Task<SharedKernel.Employee.Handlers.GetEmployee.Employee> GetEmployee(string? loginID)
        {
            _logger.LogInformation("Getting employee");
            var employee = await _mediator.Send(new GetEmployeeQuery(loginID));
            _logger.LogInformation("Retrieved employee");
            Guard.Against.Null(employee, _logger);

            return employee!;
        }

        public async Task<List<SharedKernel.Department.Handlers.GetDepartments.Department>> GetDepartments()
        {
            _logger.LogInformation("Getting departments");
            var departments = await _mediator.Send(new GetDepartmentsQuery());

            _logger.LogInformation("Returning departments");
            return departments;
        }

        public async Task<List<SharedKernel.Shift.Handlers.GetShifts.Shift>> GetShifts()
        {
            _logger.LogInformation("Getting shifts");
            var shifts = await _mediator.Send(new GetShiftsQuery());

            _logger.LogInformation("Returning shifts");
            return shifts;
        }

        public async Task<List<string>> GetJobTitles()
        {
            _logger.LogInformation("Getting job titles");
            var jobTitles = await _mediator.Send(new GetJobTitlesQuery());

            _logger.LogInformation("Returning job titles");
            return jobTitles;
        }

        public async Task UpdateEmployee(EditEmployeeViewModel viewModel)
        {
            var employee = await GetEmployee(viewModel!.Key);
            var employeeToUpdate = _mapper.Map<SharedKernel.Employee.Handlers.UpdateEmployee.Employee>(employee);
            _mapper.Map(viewModel.Employee, employeeToUpdate);

            _logger.LogInformation("Updating employee");
            await _mediator.Send(new UpdateEmployeeCommand(viewModel.Key!, employeeToUpdate));
            _logger.LogInformation("Employee updated successfully");
        }

        public async Task AddDepartmentHistory(AddDepartmentHistoryViewModel viewModel)
        {
            var command = _mapper.Map<AddDepartmentHistoryCommand>(viewModel);

            _logger.LogInformation("Adding department history");
            await _mediator.Send(command);
            _logger.LogInformation("Added department history successfully");
        }

        public async Task UpdateDepartmentHistory(UpdateDepartmentHistoryViewModel viewModel)
        {
            var command = _mapper.Map<UpdateDepartmentHistoryCommand>(viewModel);

            _logger.LogInformation("Updating department history");
            await _mediator.Send(command);
            _logger.LogInformation("Updating department history successfully");
        }

        public async Task DeleteDepartmentHistory(string loginID, string departmentName, string shiftName, DateTime startDate)
        {
            var command = new DeleteDepartmentHistoryCommand
            {
                LoginID = loginID,
                DepartmentName = departmentName,
                ShiftName = shiftName,
                StartDate = startDate
            };

            _logger.LogInformation("Deleting department history");
            await _mediator.Send(command);
            _logger.LogInformation("Deleted department history successfully");
        }
    }
}
