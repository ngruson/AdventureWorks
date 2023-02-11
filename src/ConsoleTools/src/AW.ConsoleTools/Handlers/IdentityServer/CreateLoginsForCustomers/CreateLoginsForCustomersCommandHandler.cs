using AutoMapper;
using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
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
        

        public async Task<Unit> Handle(CreateLoginsForCustomersCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all individual customers");
            var customers = await mediator.Send(
                new GetAllCustomersQuery { CustomerType = CustomerType.Individual },
                cancellationToken
            );

            logger.LogInformation("Found {Count} individual customers", customers.Count);

            int i = 1;
            foreach (var customer in customers.Cast<IndividualCustomerDto>())
            {
                if (customer.Person!.EmailAddresses.Count > 0)
                {
                    var command = mapper.Map<CreateLoginCommand>(customer);

                    logger.LogInformation("Creating login for customer {Counter} of {Total}: {@Customer}", i, customers.Count, command);

                    await mediator.Send(
                        command,
                        cancellationToken
                    );
                }
                i++;
            }

            return Unit.Value;
        }
    }
}