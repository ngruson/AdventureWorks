using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
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
    }
}
