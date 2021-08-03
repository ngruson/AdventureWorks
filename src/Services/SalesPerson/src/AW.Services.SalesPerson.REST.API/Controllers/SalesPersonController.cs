using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SalesPersonController : ControllerBase
    {
        private readonly ILogger<SalesPersonController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SalesPersonController(ILogger<SalesPersonController> logger, IMediator mediator, IMapper mapper) =>
            (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetSalesPersons([FromQuery] GetSalesPersonsQuery query)
        {
            logger.LogInformation("GetSalesPersons called");

            logger.LogInformation("Sending the GetSalesPersons query");
            var salesPersons = await mediator.Send(query);

            if (salesPersons == null || !salesPersons.Any())
            {
                logger.LogInformation("No sales persons found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales persons");
            return new OkObjectResult(mapper.Map<List<Models.SalesPerson>>(salesPersons));
        }

        [HttpGet("GetSalesPerson")]
        public async Task<IActionResult> GetSalesPerson([FromQuery] GetSalesPersonQuery query)
        {
            logger.LogInformation("GetSalesPerson called");

            logger.LogInformation("Sending the GetSalesPerson query");
            var salesPerson = await mediator.Send(query);

            if (salesPerson == null)
            {
                logger.LogInformation("No sales person found for {@Query}", query);
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales person");
            return new OkObjectResult(mapper.Map<Models.SalesPerson>(salesPerson));
        }
    }
}