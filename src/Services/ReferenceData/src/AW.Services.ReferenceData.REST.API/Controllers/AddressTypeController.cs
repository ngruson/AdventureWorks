using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AddressTypeController : ControllerBase
    {
        private readonly ILogger<AddressTypeController> logger;
        private readonly IMediator mediator;

        public AddressTypeController(
            ILogger<AddressTypeController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetAddressTypes()
        {
            logger.LogInformation("GetAddressTypes called");

            logger.LogInformation("Sending the GetAddressTypes query");
            var addressTypes = await mediator.Send(new GetAddressTypesQuery());

            logger.LogInformation("Returning address types");
            return new OkObjectResult(addressTypes);
        }
    }
}
