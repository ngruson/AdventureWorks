using AW.Services.Product.Core.Handlers.GetProductModel;
using AW.Services.Product.Core.Handlers.GetProductModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductModelController : ControllerBase
    {
        private readonly ILogger<ProductModelController> logger;
        private readonly IMediator mediator;

        public ProductModelController(
            ILogger<ProductModelController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetProductModels()
        {
            logger.LogInformation("Sending the GetProductModels query");
            var productModels = await mediator.Send(new GetProductModelsQuery());

            if (productModels == null || productModels?.Count == 0)
            {
                logger.LogInformation("No product models were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} product models", productModels?.Count);
            return Ok(productModels);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductModel([FromRoute] GetProductModelQuery query)
        {
            logger.LogInformation("Sending the GetProductModel query");
            var productModel = await mediator.Send(query);

            if (productModel == null)
            {
                logger.LogInformation("Product model '{Name}' was not found", query.Name);
                return new NotFoundResult();
            }

            logger.LogInformation("Returning product model");
            return Ok(productModel);
        }
    }
}
