using AutoMapper;
using AW.Services.ReferenceData.Application.CountryRegion.GetCountries;
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
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            logger.LogInformation("GetCountries called");

            var query = new GetCountriesQuery();
            logger.LogInformation("Sending GetCountries query");
            var countries = await mediator.Send(query);

            if (!countries.Any())
            {
                logger.LogInformation("No countries were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} countries", countries.Count());
            return new OkObjectResult(mapper.Map<IEnumerable<Country>>(countries));
        }
    }
}