using System.Net;
using AW.Services.Infrastructure.ActionResults;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.DuplicateProduct;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Handlers.UpdateProduct;
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
            try
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
            catch (ProductsNotFoundException)
            {
                logger.LogInformation("Products not found");
                return new NotFoundResult();
            }
        }

        [HttpGet("{productNumber}")]
        public async Task<ActionResult> GetProduct([FromRoute] GetProductQuery query)
        {
            try
            {
                logger.LogInformation("GetProduct called with product number {ProductNumber}",
                    query.ProductNumber
                );

                logger.LogInformation("Sending the GetProduct query");
                var product = await mediator.Send(query);

                logger.LogInformation("Returning product");
                return new OkObjectResult(product);
            }
            catch (ProductNotFoundException)
            {
                logger.LogInformation("Product {ProductNumber} was not found", query.ProductNumber);
                return new NotFoundResult();
            }           
        }

        [HttpPut("{productNumber}")]
        public async Task<IActionResult> UpdateProduct(string productNumber, Core.Handlers.UpdateProduct.Product product)
        {
            try
            {
                logger.LogInformation("UpdateProduct called with {ProductNumber}", productNumber);

                logger.LogInformation("Sending the UpdateProduct command");
                var updatedProduct = await mediator.Send(new UpdateProductCommand(product));

                logger.LogInformation("Returning updated product");
                return Ok(updatedProduct);
            }
            catch (ProductNotFoundException)
            {
                logger.LogInformation("Product {ProductNumber} was not found", productNumber);
                return new NotFoundResult();
            }
        }

        [Route("{productNumber}/duplicate")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DuplicateProduct([FromRoute] DuplicateProductCommand command)
        {
            try
            {
                logger.LogInformation("Sending the DuplicateProduct command");
                var product = await mediator.Send(command);

                return new OkObjectResult(product);
            }
            catch (ProductNotFoundException)
            {
                logger.LogError("Product not found");
                return new NotFoundResult();
            }
            catch (DuplicateProductException ex)
            {
                logger.LogError("Duplicating product failed");
                return new InternalServerErrorObjectResult(ex.Message);
            }
        }
    }
}
