using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AW.Services.HumanResources.REST.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AuthN:AzureAd:Scopes")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMediator _mediator;

        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator) =>
            (_logger, _mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery query)
        {
            _logger.LogInformation("GetEmployees called with {@Request}", query);

            _logger.LogInformation("Sending the GetEmployees query");
            var result = await _mediator.Send(query);

            if (result == null || !result.Employees.Any())
            {
                _logger.LogInformation("No employees found");
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning employees");
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee([FromQuery] GetEmployeeQuery query)
        {
            _logger.LogInformation("GetEmployee called with {@Query}", query);

            _logger.LogInformation("Sending the GetEmployee query");
            var employee = await _mediator.Send(query);

            if (employee == null)
            {
                _logger.LogInformation("Employee '{LoginID}' found", query.LoginID);
                return new NotFoundResult();
            }

            _logger.LogInformation("Returning employee");
            return Ok(employee);
        }
    }
}