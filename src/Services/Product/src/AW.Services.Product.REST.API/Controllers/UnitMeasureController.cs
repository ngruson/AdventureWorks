using AW.Services.Product.Core.Handlers.GetUnitMeasures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UnitMeasureController : ControllerBase
    {
        private readonly ILogger<UnitMeasureController> logger;
        private readonly IMediator mediator;

        public UnitMeasureController(
            ILogger<UnitMeasureController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetUnitMeasures()
        {
            logger.LogInformation("Sending the GetUnitMeasures query");
            var unitMeasures = await mediator.Send(new GetUnitMeasuresQuery());

            if (unitMeasures == null || unitMeasures.Count == 0)
            {
                logger.LogInformation("No unit measures were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} unit measures", unitMeasures.Count);
            return Ok(unitMeasures);
        }
    }
}
