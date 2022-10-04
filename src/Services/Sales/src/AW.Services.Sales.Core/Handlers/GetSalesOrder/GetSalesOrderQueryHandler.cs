using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQueryHandler : IRequestHandler<GetSalesOrderQuery, SalesOrderDto>
    {
        private readonly ILogger<GetSalesOrderQueryHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _repository;
        private readonly IMapper _mapper;

        public GetSalesOrderQueryHandler(
            ILogger<GetSalesOrderQueryHandler> logger,
            IRepository<Entities.SalesOrder> repository, IMapper mapper) =>
            (_logger, _repository, _mapper) = (logger, repository, mapper);

        public async Task<SalesOrderDto> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting sales order from database");
            var spec = new GetFullSalesOrderSpecification(
                request.SalesOrderNumber
            );

            var salesOrder = await _repository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrderNumber, _logger);

            _logger.LogInformation("Returning sales orders");
            return _mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}