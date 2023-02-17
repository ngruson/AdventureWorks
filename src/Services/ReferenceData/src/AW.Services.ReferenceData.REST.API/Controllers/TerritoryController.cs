using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        public async Task<IActionResult> GetTerritories(string? countryRegionCode)
        {
            logger.LogInformation("GetTerritories called");

            var query = new GetTerritoriesQuery
            {
                CountryRegionCode = countryRegionCode
            };
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
