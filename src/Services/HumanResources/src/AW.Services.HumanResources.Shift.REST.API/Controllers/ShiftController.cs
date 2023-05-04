using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using AW.Services.Infrastructure.ActionResults;
using AW.Services.SharedKernel.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AW.Services.HumanResources.Shift.REST.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AuthN:AzureAd:Scopes")]
    public class ShiftController : ControllerBase
    {
        private readonly ILogger<ShiftController> _logger;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateShiftCommand> _createValidator;
        private readonly IValidator<DeleteShiftCommand> _deleteValidator;
        private readonly IValidator<UpdateShiftCommand> _updateValidator;

        public ShiftController(
            ILogger<ShiftController> logger,
            IMediator mediator,
            IValidator<CreateShiftCommand> createValidator,
            IValidator<DeleteShiftCommand> deleteValidator,
            IValidator<UpdateShiftCommand> updateValidator
        ) =>
            (_logger, _mediator, _createValidator, _deleteValidator, _updateValidator) = 
                (logger, mediator, createValidator, deleteValidator, updateValidator);

        [HttpGet]
        public async Task<IActionResult> GetShifts([FromQuery] GetShiftsQuery query)
        {
            _logger.LogInformation("GetShifts called with {@Request}", query);
            _logger.LogInformation("Sending the GetShifts query");

            try
            {
                var shifts = await _mediator.Send(query);
                _logger.LogInformation("Returning shifts");
                return Ok(shifts);
            }
            catch (ShiftsNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetShift([FromRoute] GetShiftQuery query)
        {
            _logger.LogInformation("GetShift called with {@Query}", query);

            _logger.LogInformation("Sending the GetShift query");

            try
            {
                var shift = await _mediator.Send(query);
                _logger.LogInformation("Returning shift");
                return Ok(shift);
            }
            catch (ShiftNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(Core.Handlers.CreateShift.Shift shift)
        {
            try
            {
                var command = new CreateShiftCommand(shift);
                var validationResult = await _createValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the CreateShift command");
                    var addedShift = await _mediator.Send(command);

                    _logger.LogInformation("Returning added shift");
                    return Ok(addedShift);
                }

                return new BadRequestObjectResult(
                    Results.ValidationProblem(validationResult.ToDictionary())
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while adding shift: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Adding shift failed");
            }
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateShift(UpdateShiftCommand command)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the UpdateShiftCommand command");
                    var updatedShift = await _mediator.Send(command);

                    _logger.LogInformation("Returning updated shift");
                    return Ok(updatedShift);
                }

                return new BadRequestObjectResult(
                    Results.ValidationProblem(validationResult.ToDictionary())
                );
            }
            catch (ShiftNotFoundException)
            {
                _logger.LogInformation("Shift not found");
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while updating shift: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Updating shift failed");
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteShift([FromRoute] DeleteShiftCommand command)
        {
            try
            {
                var validationResult = await _deleteValidator.ValidateAsync(command);
                if (!validationResult.IsValid)
                    validationResult.AddToModelState(ModelState);

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Sending the DeleteShiftCommand command");
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
                _logger.LogError("Something went wrong while deleting shift: {Message}", ex.Message);
                return new InternalServerErrorObjectResult("Deleting shift failed");
            }
        }
    }
}
