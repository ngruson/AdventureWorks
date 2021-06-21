using AW.Services.ReferenceData.Application.Territory.GetTerritories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TerritoryController : ControllerBase
    {
        private readonly ILogger<TerritoryController> logger;
        private readonly IMediator mediator;

        public TerritoryController(
            ILogger<TerritoryController> logger,
            IMediator mediator
        ) =>
            (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetTerritories()
        {
            logger.LogInformation("GetTerritories called");

            var query = new GetTerritoriesQuery();
            logger.LogInformation("Sending GetTerritories query");
            var territories = await mediator.Send(query);

            if (!territories.Any())
            {
                logger.LogInformation("No territories were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} territories", territories.Count);
            return new OkObjectResult(territories);
        }
    }
}