using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer;
using AW.Services.SalesOrder.REST.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SalesOrderController : ControllerBase
    {
        private readonly ILogger<SalesOrderController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SalesOrderController(ILogger<SalesOrderController> logger, IMediator mediator, IMapper mapper) =>
            (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetSalesOrders([FromQuery] GetSalesOrdersQuery query)
        {
            logger.LogInformation("GetSalesOrders called");

            logger.LogInformation("Sending the GetSalesOrders query");
            var salesOrders = await mediator.Send(query);

            if (salesOrders == null || salesOrders.SalesOrders == null || !salesOrders.SalesOrders.Any())
            {
                logger.LogInformation("No sales orders found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales orders");
            return new OkObjectResult(mapper.Map<SalesOrdersResult>(salesOrders));
        }

        [HttpGet("customer/{customerNumber}")]
        public async Task<IActionResult> GetSalesOrdersForCustomer([FromRoute] GetSalesOrdersForCustomerQuery query)
        {
            logger.LogInformation("GetSalesOrdersForCustomer called");

            logger.LogInformation("Sending the GetSalesOrdersForCustomer query");
            var salesOrders = await mediator.Send(query);

            if (salesOrders == null || salesOrders.Count == 0)
            {
                logger.LogInformation("No sales orders found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales orders");
            return new OkObjectResult(mapper.Map<List<Models.SalesOrder>>(salesOrders));
        }

        [HttpGet("{salesOrderNumber}")]
        public async Task<IActionResult> GetSalesOrder([FromRoute] GetSalesOrderQuery query)
        {
            logger.LogInformation("GetSalesOrder called");

            logger.LogInformation("Sending the GetSalesOrder query");
            var salesOrder = await mediator.Send(query);

            if (salesOrder == null)
            {
                logger.LogInformation("No sales order found for {@Query}", query);
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales order");
            return new OkObjectResult(mapper.Map<Models.SalesOrder>(salesOrder));
        }
    }
}