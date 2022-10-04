using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Guards;
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
        private readonly ILogger<GetSalesOrdersForCustomerQueryHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;
        private readonly IMapper _mapper;

        public GetSalesOrdersForCustomerQueryHandler(
            ILogger<GetSalesOrdersForCustomerQueryHandler> logger,
            IRepository<Entities.SalesOrder> repository, IMapper mapper) =>
            (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<GetSalesOrdersDto> Handle(GetSalesOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting sales orders from database for customer {Customer}", request.CustomerNumber);
            var spec = new GetSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );
            var countSpec = new CountSalesOrdersForCustomerSpecification(
                request.CustomerNumber
            );

            var orders = await _repository.ListAsync(spec, cancellationToken);
            Guard.Against.SalesOrdersNull(orders, _logger);

            _logger.LogInformation("Returning sales orders");
            return new GetSalesOrdersDto
            {
                SalesOrders = _mapper.Map<List<SalesOrderDto>>(orders),
                TotalSalesOrders = await _repository.CountAsync(countSpec, cancellationToken)
            };
        }
    }
}