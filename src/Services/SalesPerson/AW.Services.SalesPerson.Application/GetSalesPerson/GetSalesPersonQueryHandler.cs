using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesPerson.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.Application.GetSalesPerson
{
    public class GetSalesPersonQueryHandler : IRequestHandler<GetSalesPersonQuery, SalesPersonDto>
    {
        private readonly ILogger<GetSalesPersonQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.SalesPerson> repository;

        public GetSalesPersonQueryHandler(ILogger<GetSalesPersonQueryHandler> logger, IMapper mapper, IRepositoryBase<Domain.SalesPerson> repository) =>
            (this.logger, this.mapper, this.repository) = (logger, mapper, repository);
        
        public async Task<SalesPersonDto> Handle(GetSalesPersonQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");
            logger.LogInformation("Getting sales person from database");

            var spec = new GetSalesPersonSpecification(
                request.FirstName,
                request.MiddleName,
                request.LastName
            );

            var salesPerson = await repository.GetBySpecAsync(spec);
            Guard.Against.Null(salesPerson, nameof(salesPerson), logger);

            logger.LogInformation("Returning sales persons");

            return mapper.Map<SalesPersonDto>(salesPerson);
        }
    }
}