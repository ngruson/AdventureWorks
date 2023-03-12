using AutoMapper;
using AW.Services.Sales.Core.Handlers.GetSalesPerson;
using AW.Services.Sales.Core.Handlers.GetSalesPersons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.Sales.SalesPerson.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SalesPersonController : ControllerBase
    {
        private readonly ILogger<SalesPersonController> logger;
        private readonly IMediator mediator;

        public SalesPersonController(ILogger<SalesPersonController> logger, IMediator mediator) =>
            (this.logger, this.mediator) = (logger, mediator);

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
            return new OkObjectResult(salesPersons);
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
            return new OkObjectResult(salesPerson);
        }
    }
}
