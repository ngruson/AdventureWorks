using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AW.Services.HumanResources.Department.REST.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AuthN:AzureAd:Scopes")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMediator _mediator;

        public DepartmentController(ILogger<DepartmentController> logger, IMediator mediator) =>
            (_logger, _mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] GetDepartmentsQuery query)
        {
            _logger.LogInformation("GetDepartments called with {@Request}", query);
            _logger.LogInformation("Sending the GetDepartments query");

            try
            {
                var departments = await _mediator.Send(query);
                _logger.LogInformation("Returning departments");
                return Ok(departments);
            }
            catch (DepartmentsNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetDepartment([FromRoute] GetDepartmentQuery query)
        {
            _logger.LogInformation("GetDepartment called with {@Query}", query);

            _logger.LogInformation("Sending the GetDepartment query");
            
            try
            {
                var department = await _mediator.Send(query);
                _logger.LogInformation("Returning department");
                return Ok(department);
            }
            catch (DepartmentNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentCommand command)
        {
            try
            {
                _logger.LogInformation("Sending the UpdateDepartmentCommand command");
                var updatedDepartment = await _mediator.Send(command);

                _logger.LogInformation("Returning updated department");
                return Ok(updatedDepartment);
            }
            catch (DepartmentNotFoundException)
            {
                _logger.LogInformation("Department not found");
                return new NotFoundResult();
            }
        }
    }
}
