using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.SalesPerson.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.Core.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQueryHandler : IRequestHandler<GetSalesPersonsQuery, List<SalesPersonDto>>
    {
        private readonly ILogger<GetSalesPersonsQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IRepository<Entities.SalesPerson> repository;

        public GetSalesPersonsQueryHandler(ILogger<GetSalesPersonsQueryHandler> logger, IRepository<Entities.SalesPerson> repository, IMapper mapper) =>
            (this.logger, this.repository, this.mapper) = (logger, repository, mapper);
        
        public async Task<List<SalesPersonDto>> Handle(GetSalesPersonsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");            

            List<Entities.SalesPerson> salesPersons;
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