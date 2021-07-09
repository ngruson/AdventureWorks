using AutoMapper;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, GetSalesOrdersDto>
    {
        private readonly ILogger<GetSalesOrdersQueryHandler> logger;
        private readonly IRepository<Entities.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersQueryHandler(
            ILogger<GetSalesOrdersQueryHandler> logger,
            IRepository<Entities.SalesOrder> repository,
            IMapper mapper) =>
                (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<GetSalesOrdersDto> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting sales orders from database");
            var spec = new GetSalesOrdersPaginatedSpecification(
                request.PageIndex,
                request.PageSize,
                request.Territory
            );
            var countSpec = new CountSalesOrdersSpecification(
                request.Territory
            );

            var orders = await repository.ListAsync(spec);

            logger.LogInformation("Returning sales orders");
            return new GetSalesOrdersDto
            {
                SalesOrders = mapper.Map<List<SalesOrderDto>>(orders),
                TotalSalesOrders = await repository.CountAsync(countSpec)
            };
        }
    }
}