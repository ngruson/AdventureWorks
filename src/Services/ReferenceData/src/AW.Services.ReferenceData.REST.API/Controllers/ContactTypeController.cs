using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.Services.ReferenceData.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactTypeController : ControllerBase
    {
        private readonly ILogger<ContactTypeController> logger;
        private readonly IMediator mediator;

        public ContactTypeController(
            ILogger<ContactTypeController> logger,
            IMediator mediator
        ) => (this.logger, this.mediator) = (logger, mediator);

        [HttpGet]
        public async Task<IActionResult> GetContactTypes()
        {
            logger.LogInformation("GetContactTypes called");

            logger.LogInformation("Sending the GetContactTypes query");
            var contactTypes = await mediator.Send(new GetContactTypesQuery());

            logger.LogInformation("Returning contact types");
            return new OkObjectResult(contactTypes);
        }
    }
}