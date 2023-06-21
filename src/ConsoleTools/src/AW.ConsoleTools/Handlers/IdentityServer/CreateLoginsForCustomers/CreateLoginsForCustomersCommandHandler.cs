using AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.ConsoleTools.Handlers.CreateLoginsForCustomers
{
    public class CreateLoginsForCustomersCommandHandler : IRequestHandler<CreateLoginsForCustomersCommand>
    {
        private readonly ILogger<CreateLoginsForCustomersCommandHandler> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateLoginsForCustomersCommandHandler(
            ILogger<CreateLoginsForCustomersCommandHandler> logger,
            IMediator mediator,
            IMapper mapper
        ) 
            => (this.logger, this.mediator, this.mapper) = (logger, mediator, mapper);
        

        public async Task Handle(CreateLoginsForCustomersCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all individual customers");
            var customers = await mediator.Send(
                new GetCustomersQuery(CustomerType.Individual),
                cancellationToken
            );

            logger.LogInformation("Found {Count} individual customers", customers.Value.Count);

            int i = 1;
            foreach (var customer in customers.Value.Cast<IndividualCustomer>())
            {
                if (customer.Person!.EmailAddresses.Count > 0)
                {
                    var command = mapper.Map<CreateLoginCommand>(customer);

                    logger.LogInformation(
                        "Creating login for customer {Counter} of {Total}: {@Customer}", 
                        i, 
                        customers.Value.Count, 
                        command
                    );

                    await mediator.Send(
                        command,
                        cancellationToken
                    );
                }
                i++;
            }
        }
    }
}
