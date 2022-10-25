using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AW.Services.HumanResources.Employee.REST.API.Controllers
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

            try
            {
                var result = await _mediator.Send(query);
                _logger.LogInformation("Returning employees");
                return Ok(result);
            }
            catch (EmployeesNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("{loginID}")]
        public async Task<IActionResult> GetEmployee([FromRoute] GetEmployeeQuery query)
        {
            _logger.LogInformation("GetEmployee called with {@Query}", query);

            _logger.LogInformation("Sending the GetEmployee query");
            
            try
            {
                var employee = await _mediator.Send(query);
                _logger.LogInformation("Returning employee");
                return Ok(employee);
            }
            catch (EmployeeNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}