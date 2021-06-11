using Ardalis.Specification;
using AutoMapper;
using AW.Services.SalesOrder.Application.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.SalesOrder.Application.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQueryHandler : IRequestHandler<GetSalesOrdersForCustomerQuery, List<SalesOrderDto>>
    {
        private readonly ILogger<GetSalesOrdersForCustomerQueryHandler> logger;
        private readonly IRepositoryBase<Domain.SalesOrder> repository;
        private readonly IMapper mapper;

        public GetSalesOrdersForCustomerQueryHandler(
            ILogger<GetSalesOrdersForCustomerQueryHandler> logger,
            IRepositoryBase<Domain.SalesOrder> repository, IMapper mapper) =>
            (this.logger, this.repository, this.mapper) = (logger, repository, mapper);

        public async Task<List<SalesOrderDto>> Handle(GetSalesOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting sales orders from database for customer {Customer}", request.CustomerNumber);
            var spec = new GetSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );

            var orders = await repository.ListAsync(spec);

            logger.LogInformation("Returning sales orders");
            return mapper.Map<List<SalesOrderDto>>(orders);
        }
    }
}