using AutoMapper;
using AW.Services.Product.Application.GetProduct;
using AW.Services.Product.Application.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator,
            IMapper mapper
        ) => (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Product>>> GetProducts(int pageIndex, int pageSize)
        {
            logger.LogInformation("GetProducts called. pageIndex = {PageIndex}, pageSize = {PageSize}",
                pageIndex, pageSize
            );

            var query = new GetProductsQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            logger.LogInformation("Sending the GetProducts query");
            var dto = await mediator.Send(query);

            if (!dto.Any())
            {
                logger.LogInformation("No products were found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning {Count} products", dto.Count());
            return new OkObjectResult(mapper.Map<IEnumerable<Models.Product>>(dto));
        }

        [HttpGet("{productNumber}")]
        public async Task<ActionResult<Models.Product>> GetProduct(string productNumber)
        {
            logger.LogInformation("GetProduct called with product number {ProductNumber}",
                productNumber
            );

            var query = new GetProductQuery
            {
                ProductNumber = productNumber
            };

            logger.LogInformation("Sending the GetProduct query");
            var dto = await mediator.Send(query);

            if (dto == null)
            {
                logger.LogInformation("Product was not found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning product");
            return new OkObjectResult(mapper.Map<Models.Product>(dto));
        }        
    }
}