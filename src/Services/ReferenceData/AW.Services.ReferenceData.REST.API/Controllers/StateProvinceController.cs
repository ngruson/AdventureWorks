using AutoMapper;
using AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateProvinceController : ControllerBase
    {
        private readonly ILogger<StateProvinceController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public StateProvinceController(
            ILogger<StateProvinceController> logger,
            IMediator mediator,
            IMapper mapper
        ) =>
            (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateProvince>>> GetStateProvinces(string countryRegionCode)
        {
            logger.LogInformation("GetStateProvinces called");

            var query = new GetStateProvincesQuery { CountryRegionCode = countryRegionCode };
            logger.LogInformation("Sending GetStateProvinces query");
            var stateProvinces = await mediator.Send(query);

            if (!stateProvinces.Any())
            {
                logger.LogInformation("No state/provinces were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} state/provinces", stateProvinces.Count());
            return new OkObjectResult(stateProvinces);
        }
    }
}