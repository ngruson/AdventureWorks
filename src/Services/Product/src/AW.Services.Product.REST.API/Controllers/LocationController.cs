using AW.Services.Product.Core.Handlers.GetLocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> logger;
        private readonly IMediator mediator;

        public LocationController(
            ILogger<LocationController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetLocations([FromQuery] GetLocationsQuery query)
        {
            logger.LogInformation("Sending the GetLocations query");
            var locations = await mediator.Send(query);

            if (locations == null || locations?.Count == 0)
            {
                logger.LogInformation("No locations were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} locations", locations?.Count);
            return Ok(locations);
        }
    }
}
