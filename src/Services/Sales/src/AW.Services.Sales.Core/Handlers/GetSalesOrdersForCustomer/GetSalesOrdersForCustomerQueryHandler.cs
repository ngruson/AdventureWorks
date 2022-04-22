using AutoMapper;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQueryHandler : IRequestHandler<GetSalesOrdersForCustomerQuery, GetSalesOrdersDto>
    {
        private readonly ILogger<GetSalesOrdersForCustomerQueryHandler> logger;
        private readonly IRepository<Entities.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersForCustomerQueryHandler(
            ILogger<GetSalesOrdersForCustomerQueryHandler> logger,
            IRepository<Entities.SalesOrder> repository, IMapper mapper) =>
            (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<GetSalesOrdersDto> Handle(GetSalesOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting sales orders from database for customer {Customer}", request.CustomerNumber);
            var spec = new GetSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );
            var countSpec = new CountSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );

            var orders = await repository.ListAsync(spec, cancellationToken);

            logger.LogInformation("Returning sales orders");
            return new GetSalesOrdersDto
            {
                SalesOrders = mapper.Map<List<SalesOrderDto>>(orders),
                TotalSalesOrders = await repository.CountAsync(countSpec, cancellationToken)
            };
        }
    }
}