using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.Infrastructure.ActionResults;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        private readonly IValidator<CreateDepartmentCommand> _createValidator;
        private readonly IValidator<DeleteDepartmentCommand> _deleteValidator;
        private readonly IValidator<UpdateDepartmentCommand> _updateValidator;

        public DepartmentController(
            ILogger<DepartmentController> logger, 
            IMediator mediator,
            IValidator<CreateDepartmentCommand> createValidator,
            IValidator<DeleteDepartmentCommand> deleteValidator,
            IValidator<UpdateDepartmentCommand> updateValidator
        ) =>
            (_logger, _mediator, _createValidator, _deleteValidator, _updateValidator) = 
                (logger, mediator, createValidator, deleteValidator, updateValidator);

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

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Core.Handlers.CreateDepartment.Department department)
        {
            try
            {
                var command = new CreateDepartmentCommand(department);
                var validationResult = await _createValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the AddDepartmentCommand command");
                    var addedDepartment = await _mediator.Send(command);

                    _logger.LogInformation("Returning added department");
                    return Ok(addedDepartment);
                }

                return new BadRequestObjectResult(
                    Results.ValidationProblem(validationResult.ToDictionary())
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while adding department: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Adding department failed");
            }
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentCommand command)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the UpdateDepartmentCommand command");
                    var updatedDepartment = await _mediator.Send(command);

                    _logger.LogInformation("Returning updated department");
                    return Ok(updatedDepartment);
                }

                return new BadRequestObjectResult(
                    Results.ValidationProblem(validationResult.ToDictionary())
                );
            }
            catch (DepartmentNotFoundException)
            {
                _logger.LogInformation("Department not found");
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating department: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Updating department failed");
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] DeleteDepartmentCommand command)
        {
            try
            {
                var validationResult = await _deleteValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the DeleteDepartmentCommand command");
                    await _mediator.Send(command);

                    _logger.LogInformation("Returning OK");
                    return Ok();
                }

                return new BadRequestObjectResult(
                    Results.ValidationProblem(validationResult.ToDictionary())
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while deleting department: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Deleting department failed");
            }
        }
    }
}
