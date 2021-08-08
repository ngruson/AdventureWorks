using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
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
    public class StateProvinceController : ControllerBase
    {
        private readonly ILogger<StateProvinceController> logger;
        private readonly IMediator mediator;

        public StateProvinceController(
            ILogger<StateProvinceController> logger,
            IMediator mediator
        ) =>
            (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetStatesProvinces(string countryRegionCode)
        {
            logger.LogInformation("GetStateProvinces called");

            var query = new GetStatesProvincesQuery { CountryRegionCode = countryRegionCode };
            logger.LogInformation("Sending GetStateProvinces query");
            var stateProvinces = await mediator.Send(query);

            if (!stateProvinces.Any())
            {
                logger.LogInformation("No state/provinces were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} state/provinces", stateProvinces.Count);
            return new OkObjectResult(stateProvinces);
        }
    }
}