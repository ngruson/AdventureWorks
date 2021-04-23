using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesPerson.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.Application.GetSalesPersons
{
    public class GetSalesPersonsQueryHandler : IRequestHandler<GetSalesPersonsQuery, List<SalesPersonDto>>
    {
        private readonly ILogger<GetSalesPersonsQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.SalesPerson> repository;

        public GetSalesPersonsQueryHandler(ILogger<GetSalesPersonsQueryHandler> logger, IMapper mapper, IRepositoryBase<Domain.SalesPerson> repository) =>
            (this.logger, this.mapper, this.repository) = (logger, mapper, repository);
        
        public async Task<List<SalesPersonDto>> Handle(GetSalesPersonsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");            

            List<Domain.SalesPerson> salesPersons;
            if (string.IsNullOrEmpty(request.Territory))
            {
                logger.LogInformation("Getting all sales persons from database");

                var spec = new GetSalesPersonsSpecification();
                salesPersons = await repository.ListAsync(spec);
            }
            else
            {
                logger.LogInformation("Getting sales persons for {Territory} from database", request.Territory);

                var spec = new GetSalesPersonsSpecification(
                    request.Territory
                );

                salesPersons = await repository.ListAsync(spec);
            }
            
            Guard.Against.Null(salesPersons, nameof(salesPersons), logger);

            logger.LogInformation("Returning sales persons");

            return mapper.Map<List<SalesPersonDto>>(salesPersons);
        }
    }
}