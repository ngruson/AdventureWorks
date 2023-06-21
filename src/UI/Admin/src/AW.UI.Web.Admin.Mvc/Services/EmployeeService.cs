using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.CreateEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetJobTitles;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services;

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

    public async Task<EmployeeViewModel> GetDetail(Guid objectId)
    {
        var employee = await GetEmployee(objectId);
        return _mapper.Map<EmployeeViewModel>(employee);
    }

    private async Task<Infrastructure.Api.Employee.Handlers.GetEmployee.Employee> GetEmployee(Guid objectId)
    {
        _logger.LogInformation("Getting employee");
        var employee = await _mediator.Send(new GetEmployeeQuery(objectId));
        _logger.LogInformation("Retrieved employee");
        Guard.Against.Null(employee, _logger);

        return employee!;
    }

    public async Task<List<Infrastructure.Api.Department.Handlers.GetDepartments.Department>> GetDepartments()
    {
        _logger.LogInformation("Getting departments");
        var departments = await _mediator.Send(new GetDepartmentsQuery());

        _logger.LogInformation("Returning departments");
        return departments;
    }

    public async Task<List<Infrastructure.Api.Shift.Handlers.GetShifts.Shift>> GetShifts()
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

    public async Task CreateEmployee(EmployeeViewModel viewModel)
    {
        var employee = _mapper.Map<Infrastructure.Api.Employee.Handlers.CreateEmployee.Employee>(viewModel);

        _logger.LogInformation("Send command to create employee");
        await _mediator.Send(new CreateEmployeeCommand(employee));
        _logger.LogInformation("Command was succesfully executed");
    }

    public async Task<EmployeeViewModel> UpdateEmployee(EmployeeViewModel viewModel)
    {
        var employee = await GetEmployee(viewModel.ObjectId);
        var employeeToUpdate = _mapper.Map<Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee>(employee);
        _mapper.Map(viewModel, employeeToUpdate);

        _logger.LogInformation("Updating employee");
        var updatedEmployee = await _mediator.Send(new UpdateEmployeeCommand(employeeToUpdate));
        _logger.LogInformation("Employee updated successfully");

        return _mapper.Map<EmployeeViewModel>(updatedEmployee);
    }

    public async Task DeleteEmployee(Guid objectId)
    {
        _logger.LogInformation("Deleting employee");
        await _mediator.Send(new DeleteEmployeeCommand(objectId));
        _logger.LogInformation("Employee successfully deleted");
    }

    public async Task<EmployeeViewModel> AddDepartmentHistory(EditDepartmentHistoryViewModel viewModel)
    {
        var command = _mapper.Map<AddDepartmentHistoryCommand>(viewModel);

        _logger.LogInformation("Adding department history");
        await _mediator.Send(command);
        _logger.LogInformation("Added department history successfully");

        var employee = await GetDetail(viewModel.Employee);
        return employee;
    }

    public async Task<EmployeeViewModel> UpdateDepartmentHistory(EditDepartmentHistoryViewModel viewModel)
    {
        var command = _mapper.Map<UpdateDepartmentHistoryCommand>(viewModel);

        _logger.LogInformation("Updating department history");
        await _mediator.Send(command);
        _logger.LogInformation("Updating department history successfully");

        var employee = await GetDetail(viewModel.Employee);
        return employee;
    }

    public async Task<EmployeeViewModel> DeleteDepartmentHistory(Guid employee, Guid objectId)
    {
        var command = new DeleteDepartmentHistoryCommand
        {
            Employee = employee,
            ObjectId = objectId
        };

        _logger.LogInformation("Deleting department history");
        await _mediator.Send(command);
        _logger.LogInformation("Deleted department history successfully");

        var emp = await GetDetail(employee);
        return emp;
    }
}
