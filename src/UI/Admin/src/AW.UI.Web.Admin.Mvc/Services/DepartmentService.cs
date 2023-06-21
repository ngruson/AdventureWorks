using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.CreateDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services;

public class DepartmentService : IDepartmentService
{
    private readonly ILogger<DepartmentService> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DepartmentService(
        ILogger<DepartmentService> logger,
        IMapper mapper,
        IMediator mediator
    )
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<List<DepartmentViewModel>> GetDepartments()
    {
        _logger.LogInformation("Getting departments");
        var response = await _mediator.Send(new GetDepartmentsQuery());

        var vm = _mapper.Map<List<DepartmentViewModel>>(response);

        _logger.LogInformation("Returning {ViewModel}", vm);
        return vm;
    }

    public async Task<DepartmentViewModel> GetDetail(Guid objectId)
    {
        var department = await GetDepartment(objectId);
        var vm = _mapper.Map<DepartmentViewModel>(department);

        _logger.LogInformation("Returning department");
        return vm;
    }

    public async Task CreateDepartment(DepartmentViewModel viewModel)
    {
        var department = _mapper.Map<Infrastructure.Api.Department.Handlers.CreateDepartment.Department>(viewModel);

        _logger.LogInformation("Send command to add department");
        await _mediator.Send(new CreateDepartmentCommand(department));
        _logger.LogInformation("Command was succesfully executed");
    }

    public async Task UpdateDepartment(DepartmentViewModel viewModel)
    {
        var department = await GetDepartment(viewModel.ObjectId);
        var departmentToUpdate = _mapper.Map<Infrastructure.Api.Department.Handlers.UpdateDepartment.Department>(department);
        _mapper.Map(viewModel, departmentToUpdate);

        _logger.LogInformation("Updating department");
        await _mediator.Send(new UpdateDepartmentCommand(departmentToUpdate));
        _logger.LogInformation("Department updated successfully");
    }

    private async Task<Infrastructure.Api.Department.Handlers.GetDepartment.Department> GetDepartment(Guid objectId)
    {
        _logger.LogInformation("Getting department");
        var department = await _mediator.Send(new GetDepartmentQuery(objectId));
        _logger.LogInformation("Retrieved department");
        Guard.Against.Null(department, _logger);

        return department!;
    }

    public async Task DeleteDepartment(Guid objectId)
    {
        _logger.LogInformation("Deleting department");
        await _mediator.Send(new DeleteDepartmentCommand(objectId));
        _logger.LogInformation("Department successfully deleted");
    }
}
