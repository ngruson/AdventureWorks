using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.GetSalesPerson
{
    public class GetSalesPersonQueryHandler : IRequestHandler<GetSalesPersonQuery, SalesPersonDto>
    {
        private readonly ILogger<GetSalesPersonQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.SalesPerson> repository;

        public GetSalesPersonQueryHandler(
            ILogger<GetSalesPersonQueryHandler> logger, 
            IRepository<Entities.SalesPerson> repository, 
            IMapper mapper
        ) => (this.logger, this.repository, this.mapper) = (logger, repository, mapper);
        
        public async Task<SalesPersonDto> Handle(GetSalesPersonQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting sales person from database");

            var spec = new GetSalesPersonSpecification(
                request.FirstName,
                request.MiddleName,
                request.LastName
            );

            var salesPerson = await repository.GetBySpecAsync(spec, cancellationToken);
            Guard.Against.Null(salesPerson, nameof(salesPerson), logger);

            logger.LogInformation("Returning sales persons");

            return mapper.Map<SalesPersonDto>(salesPerson);
        }
    }
}