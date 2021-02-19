using AutoMapper;
using AW.Services.CountryRegion.Application.GetCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.CountryRegion.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryRegionController : ControllerBase
    {
        private readonly ILogger<CountryRegionController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CountryRegionController(
            ILogger<CountryRegionController> logger,
            IMediator mediator,
            IMapper mapper
        ) =>
            (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.CountryRegion>>> GetCountries()
        {
            logger.LogInformation("GetCountries called");

            var query = new GetCountriesQuery();            
            logger.LogInformation("Sending GetCountries query");
            var dto = await mediator.Send(query);

            if (!dto.Any())
            {
                logger.LogInformation("No countries were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} countries", dto.Count());
            return new OkObjectResult(mapper.Map<IEnumerable<Models.CountryRegion>>(dto));
        }
    }
}