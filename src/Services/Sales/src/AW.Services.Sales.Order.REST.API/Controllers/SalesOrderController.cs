using AutoMapper;
using AW.Services.Infrastructure.EventBus.Extensions;
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;
using AW.Services.Sales.Core.Handlers.CancelSalesOrder;
using AW.Services.Sales.Core.Handlers.DeleteSalesOrder;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Handlers.GetSalesOrders;
using AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Handlers.RejectSalesOrder;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AW.Services.Sales.Order.REST.API.Controllers
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

            if (salesOrders == null || salesOrders.SalesOrders.Count == 0)
            {
                logger.LogInformation("No sales orders found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning sales orders");
            return new OkObjectResult(mapper.Map<SalesOrdersResult>(salesOrders));
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
            return new OkObjectResult(mapper.Map<Core.Models.SalesOrder>(salesOrder));
        }

        [HttpDelete("{salesOrderNumber}")]
        public async Task<IActionResult> DeleteSalesOrder([FromRoute] DeleteSalesOrderCommand command)
        {
            logger.LogInformation("DeleteSalesOrder called");

            logger.LogInformation("Sending the DeleteSalesOrder command");
            await mediator.Send(command);

            return new OkResult();
        }

        [Route("approve")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApproveSalesOrderAsync([FromRoute] ApproveSalesOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestApproveSalesOrder = new IdentifiedCommand<ApproveSalesOrderCommand, bool>(command, guid);

                logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestApproveSalesOrder.GetGenericTypeName(),
                    nameof(requestApproveSalesOrder.Command.SalesOrderNumber),
                    requestApproveSalesOrder.Command.SalesOrderNumber,
                    requestApproveSalesOrder
                );

                commandResult = await mediator.Send(requestApproveSalesOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("reject")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RejectSalesOrderAsync([FromRoute] RejectSalesOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestRejectSalesOrder = new IdentifiedCommand<RejectSalesOrderCommand, bool>(command, guid);

                logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestRejectSalesOrder.GetGenericTypeName(),
                    nameof(requestRejectSalesOrder.Command.SalesOrderNumber),
                    requestRejectSalesOrder.Command.SalesOrderNumber,
                    requestRejectSalesOrder
                );

                commandResult = await mediator.Send(requestRejectSalesOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelSalesOrderAsync([FromRoute] CancelSalesOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelSalesOrder = new IdentifiedCommand<CancelSalesOrderCommand, bool>(command, guid);

                logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelSalesOrder.GetGenericTypeName(),
                    nameof(requestCancelSalesOrder.Command.SalesOrderNumber),
                    requestCancelSalesOrder.Command.SalesOrderNumber,
                    requestCancelSalesOrder
                );

                commandResult = await mediator.Send(requestCancelSalesOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipSalesOrderAsync([FromRoute] ShipSalesOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestShipSalesOrder = new IdentifiedCommand<ShipSalesOrderCommand, bool>(command, guid);

                logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestShipSalesOrder.GetGenericTypeName(),
                    nameof(requestShipSalesOrder.Command.SalesOrderNumber),
                    requestShipSalesOrder.Command.SalesOrderNumber,
                    requestShipSalesOrder
                );

                commandResult = await mediator.Send(requestShipSalesOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}