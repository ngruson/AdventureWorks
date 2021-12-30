using AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ShipMethodController
    {
        private readonly ILogger<ShipMethodController> logger;
        private readonly IMediator mediator;

        public ShipMethodController(
            ILogger<ShipMethodController> logger,
            IMediator mediator
        ) =>
            (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetShipMethods()
        {
            logger.LogInformation("GetShipMethods called");

            var query = new GetShipMethodsQuery();
            logger.LogInformation("Sending GetShipMethods query");
            var shipMethods = await mediator.Send(query);

            if (!shipMethods.Any())
            {
                logger.LogInformation("No shipping methods were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} shipping methods", shipMethods.Count);
            return new OkObjectResult(shipMethods);
        }
    }
}