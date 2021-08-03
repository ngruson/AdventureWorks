using AW.Services.Product.Core.Handlers.GetProductCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Services.Product.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> logger;
        private readonly IMediator mediator;

        public ProductCategoryController(
            ILogger<ProductCategoryController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            logger.LogInformation("GetProductCategories called");

            logger.LogInformation("Sending the GetProductCategories query");
            var categories = await mediator.Send(new GetProductCategoriesQuery());

            logger.LogInformation("Returning product categories");
            return new OkObjectResult(categories);
        }
    }
}