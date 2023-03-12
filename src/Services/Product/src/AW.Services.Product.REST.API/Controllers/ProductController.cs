using AutoMapper;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IMediator mediator;

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
        {
            string logMessage = "Getting products with page index {PageIndex}, page size {PageSize}";
            var args = new List<object> { query.PageIndex, query.PageSize };

            if (!string.IsNullOrEmpty(query.Category))
            {
                logMessage += ", category {Category}";
                args.Add(query.Category);
            }
            if (!string.IsNullOrEmpty(query.Subcategory))
            {
                logMessage += ", subcategory {Subcategory}";
                args.Add(query.Subcategory);
            }

            logger.LogInformation(logMessage, args.ToArray());

            logger.LogInformation("Sending the GetProducts query");
            var result = await mediator.Send(query);

            if (result == null || result.Products == null || !result.Products.Any())
            {
                logger.LogInformation("No products were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} products", result.Products.Count);
            return Ok(result);
        }

        [HttpGet("{productNumber}")]
        public async Task<ActionResult> GetProduct([FromRoute] GetProductQuery query)
        {
            logger.LogInformation("GetProduct called with product number {ProductNumber}",
                query.ProductNumber
            );

            logger.LogInformation("Sending the GetProduct query");
            var product = await mediator.Send(query);

            if (product == null)
            {
                logger.LogInformation("Product was not found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning product");
            return new OkObjectResult(product);
        }
    }
}
