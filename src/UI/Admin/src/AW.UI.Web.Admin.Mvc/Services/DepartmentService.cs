using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;
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
    }
}
