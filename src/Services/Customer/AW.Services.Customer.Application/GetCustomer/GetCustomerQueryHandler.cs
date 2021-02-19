using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.Customer.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ILogger<GetCustomerQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Customer> repository;

        public GetCustomerQueryHandler(
            ILogger<GetCustomerQueryHandler> logger,
            IMapper mapper,
            IRepositoryBase<Domain.Customer> repository
        ) => (this.logger, this.mapper, this.repository) = (logger, mapper, repository);

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting customer from database");

            var spec = new GetCustomerSpecification(
                request.AccountNumber
            );

            var customer = await repository.GetBySpecAsync(spec);
            Guard.Against.Null(customer, nameof(customer));

            logger.LogInformation("Returning customer");
            return mapper.Map<CustomerDto>(customer);
        }
    }
}