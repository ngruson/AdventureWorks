using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.Services.HumanResources.Core.Handlers.GetJobTitles;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                var result = await _mediator.Send(query);
                _logger.LogInformation("Returning employee");
                return Ok(result.Value);
            }
            catch (EmployeeNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("jobTitles")]
        public async Task<IActionResult> GetJobTitles([FromQuery] GetJobTitlesQuery query)
        {
            _logger.LogInformation("Sending the GetJobTitles query");

            try
            {
                var result = await _mediator.Send(query);
                _logger.LogInformation("Returning job titles");
                return Ok(result);
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }

        [HttpPut("{loginID}")]
        public async Task<IActionResult> UpdateEmployee(string loginID, Core.Handlers.UpdateEmployee.Employee employee)
        {
            try
            {
                _logger.LogInformation("Sending the UpdateEmployee command");
                var updatedEmployee = await _mediator.Send(new UpdateEmployeeCommand(loginID, employee));

                _logger.LogInformation("Returning updated employee");
                return Ok(updatedEmployee);
            }
            catch (EmployeeNotFoundException)
            {
                _logger.LogInformation("Employee not found");
                return new NotFoundResult();
            }
        }

        [HttpPost("departmentHistory")]
        public async Task<IActionResult> AddDepartmentHistory(Core.Handlers.AddDepartmentHistory.AddDepartmentHistoryCommand command)
        {
            try
            {
                _logger.LogInformation("Sending the AddDepartmentHistory command");
                await _mediator.Send(command);

                _logger.LogInformation("Returning OkResult");
                return Ok();
            }
            catch (EmployeeNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Employee not found)");
                return new NotFoundResult();
            }
            catch (DepartmentNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Department not found)");
                return new NotFoundResult();
            }
            catch (ShiftNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Shift not found)");
                return new NotFoundResult();
            }
        }

        [HttpPut("departmentHistory")]
        public async Task<IActionResult> UpdateDepartmentHistory(Core.Handlers.UpdateDepartmentHistory.UpdateDepartmentHistoryCommand command)
        {
            try
            {
                _logger.LogInformation("Sending the UpdateDepartmentHistory command");
                await _mediator.Send(command);

                _logger.LogInformation("Returning OkResult");
                return Ok();
            }
            catch (EmployeeNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Employee not found)");
                return new NotFoundResult();
            }
            catch (DepartmentNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Department not found)");
                return new NotFoundResult();
            }
            catch (ShiftNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Shift not found)");
                return new NotFoundResult();
            }
        }

        [HttpDelete("departmentHistory")]
        public async Task<IActionResult> DeleteDepartmentHistory([FromQuery] Core.Handlers.DeleteDepartmentHistory.DeleteDepartmentHistoryCommand command)
        {
            try
            {
                _logger.LogInformation("Sending the DeleteDepartmentHistory command");
                await _mediator.Send(command);

                _logger.LogInformation("Returning OkResult");
                return Ok();
            }
            catch (EmployeeNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Employee not found)");
                return new NotFoundResult();
            }
            catch (EmployeeDepartmentHistoryNotFoundException)
            {
                _logger.LogInformation("Returning NotFoundResult (Department history not found)");
                return new NotFoundResult();
            }
        }
    }
}
