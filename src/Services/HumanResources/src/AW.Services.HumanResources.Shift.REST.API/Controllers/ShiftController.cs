using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
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

        public ShiftController(ILogger<ShiftController> logger, IMediator mediator) =>
            (_logger, _mediator) = (logger, mediator);

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

        [HttpGet]
        public async Task<IActionResult> GetShift([FromQuery] GetShiftQuery query)
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
    }
}