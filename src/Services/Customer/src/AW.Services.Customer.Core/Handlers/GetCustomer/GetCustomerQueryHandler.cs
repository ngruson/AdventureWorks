using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ILogger<GetCustomerQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Customer> repository;

        public GetCustomerQueryHandler(
            ILogger<GetCustomerQueryHandler> logger,
            IMapper mapper,
            IRepository<Entities.Customer> repository
        ) => (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var spec = new GetCustomerSpecification(
                request.AccountNumber
            );

            var customer = await repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning customer");
            return mapper.Map<CustomerDto>(customer);
        }
    }
}