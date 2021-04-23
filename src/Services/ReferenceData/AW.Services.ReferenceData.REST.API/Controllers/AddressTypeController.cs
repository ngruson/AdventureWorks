using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressTypeController : ControllerBase
    {
        private readonly ILogger<AddressTypeController> logger;
        private readonly IMediator mediator;

        public AddressTypeController(
            ILogger<AddressTypeController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<ActionResult<List<AddressType>>> GetAddressTypes()
        {
            logger.LogInformation("GetAddressTypes called");

            logger.LogInformation("Sending the GetAddressTypes query");
            var addressTypes = await mediator.Send(new GetAddressTypesQuery());

            logger.LogInformation("Returning address types");
            return new OkObjectResult(addressTypes);
        }
    }
}