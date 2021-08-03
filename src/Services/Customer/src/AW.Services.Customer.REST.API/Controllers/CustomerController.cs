using AutoMapper;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.Customer.REST.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator, IMapper mapper) =>
            (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] GetCustomersQuery query)
        {
            logger.LogInformation("GetCustomers called with {@Request}", query);

            logger.LogInformation("Sending the GetCustomers query");
            var customers = await mediator.Send(query);

            if (customers == null || !customers.Customers.Any())
            {
                logger.LogInformation("No customers found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning customers");
            return Ok(mapper.Map<Models.GetCustomers.GetCustomersResult>(customers));
        }

        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetCustomer([FromRoute] GetCustomerQuery query)
        {
            logger.LogInformation("GetCustomer called with query {Query}", query);

            logger.LogInformation("Sending the GetCustomer query");
            var customer = await mediator.Send(query);

            if (customer == null)
            {
                logger.LogInformation("Customer was not found");
                return new NotFoundResult();
            }

            logger.LogInformation("Returning customer");
            return Ok(mapper.Map<Models.GetCustomer.Customer>(customer));
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerCommand command)
        {
            logger.LogInformation("AddCustomer called");

            logger.LogInformation("Sending the AddCustomer command");
            var customer = await mediator.Send(command);

            logger.LogInformation("Returning customer");
            return Created($"/{customer.AccountNumber}", customer);
        }

        [HttpPut("{accountNumber}")]
        public async Task<IActionResult> UpdateCustomer(string accountNumber, Models.UpdateCustomer.Customer customer)
        {
            logger.LogInformation("UpdateCustomer called");

            logger.LogInformation("Sending the UpdateCustomer command");
            var command = mapper.Map<UpdateCustomerCommand>(customer);
            command.Customer.AccountNumber = accountNumber;
            var updatedCustomer = await mediator.Send(command);

            logger.LogInformation("Returning customer");
            return Ok(mapper.Map<Models.UpdateCustomer.Customer>(updatedCustomer));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(DeleteCustomerCommand command)
        {
            logger.LogInformation("DeleteCustomer called with account number {AccountNumber}", command.AccountNumber);

            logger.LogInformation("Sending the DeleteCustomer command");
            await mediator.Send(command);
            logger.LogInformation("Customer deleted");

            return NoContent();
        }
    }
}