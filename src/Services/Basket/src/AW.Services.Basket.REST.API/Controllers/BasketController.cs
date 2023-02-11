using AW.Services.Basket.Core.Handlers.Checkout;
using AW.Services.Basket.Core.Handlers.DeleteBasket;
using AW.Services.Basket.Core.Handlers.GetBasket;
using AW.Services.Basket.Core.Handlers.UpdateBasket;
using AW.Services.Basket.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AW.Services.Basket.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> logger;
        private readonly IMediator mediator;

        public BasketController(
            ILogger<BasketController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasketByIdAsync(string id)
        {
            logger.LogInformation("Sending GetBasket query for {UserId}", id);
            var basket = await mediator.Send(new GetBasketQuery(id));

            logger.LogInformation("Returning HTTP 200 (OK) with basket");
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasketAsync([FromBody] CustomerBasket value)
        {
            logger.LogInformation("Sending UpdateBasket command for {UserId}", value.BuyerId);
            var basket = await mediator.Send(new UpdateBasketCommand(value));

            logger.LogInformation("Returning HTTP 200 (OK) with basket");
            return Ok(basket);
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : basketCheckout.RequestId;

            logger.LogInformation("Sending Checkout command for {UserId}", basketCheckout.Buyer);
            var basket = await mediator.Send(
                new CheckoutCommand(
                    basketCheckout                
                )
            );

            if (basket == null)
            {
                logger.LogError("Basket not found for {UserId}", basketCheckout.Buyer);
                return BadRequest();
            }

            logger.LogInformation("Returning HTTP 202 (Accepted)");
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            logger.LogInformation("Deleting basket for {UserId}", id);
            await mediator.Send(new DeleteBasketCommand(id));
        }
    }
}