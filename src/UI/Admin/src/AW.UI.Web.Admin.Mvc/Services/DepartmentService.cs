using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.SharedKernel.Department.Handlers.CreateDepartment;
using AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartment;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;
using AW.UI.Web.SharedKernel.Department.Handlers.UpdateDepartment;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
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

        public async Task<DepartmentDetailViewModel> GetDetail(string name)
        {
            var department = await GetDepartment(name);

            var vm = new DepartmentDetailViewModel
            {
                Department = _mapper.Map<DepartmentViewModel>(department)
            };

            _logger.LogInformation("Returning department");
            return vm;
        }

        public async Task CreateDepartment(CreateDepartmentViewModel viewModel)
        {
            var department = _mapper.Map<SharedKernel.Department.Handlers.CreateDepartment.Department>(viewModel.Department);

            _logger.LogInformation("Send command to add department");
            await _mediator.Send(new CreateDepartmentCommand(department));
            _logger.LogInformation("Command was succesfully executed");
        }

        public async Task UpdateDepartment(EditDepartmentViewModel viewModel)
        {
            var department = await GetDepartment(viewModel.Key!);
            var departmentToUpdate = _mapper.Map<SharedKernel.Department.Handlers.UpdateDepartment.Department>(department);
            _mapper.Map(viewModel.Department, departmentToUpdate);

            _logger.LogInformation("Updating employee");
            await _mediator.Send(new UpdateDepartmentCommand(viewModel.Key!, departmentToUpdate));
            _logger.LogInformation("Employee updated successfully");
        }

        private async Task<SharedKernel.Department.Handlers.GetDepartment.Department> GetDepartment(string name)
        {
            _logger.LogInformation("Getting department");
            var department = await _mediator.Send(new GetDepartmentQuery(name));
            _logger.LogInformation("Retrieved department");
            Guard.Against.Null(department, _logger);

            return department!;
        }

        public async Task DeleteDepartment(string name)
        {
            _logger.LogInformation("Deleting department");
            await _mediator.Send(new DeleteDepartmentCommand(name));
            _logger.LogInformation("Department successfully deleted");
        }
    }
}
