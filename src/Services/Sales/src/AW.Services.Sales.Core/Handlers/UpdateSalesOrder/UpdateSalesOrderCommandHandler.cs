using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, SalesOrderDto>
    {
        private readonly ILogger<UpdateSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _salesOrderRepository;
        private readonly IRepository<Entities.SalesPerson> _salesPersonRepository;
        private readonly IMapper _mapper;

        public UpdateSalesOrderCommandHandler(
            ILogger<UpdateSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository,
            IRepository<Entities.SalesPerson> salesPersonRepository,
            IMapper mapper) =>
                (_logger, _salesOrderRepository, _salesPersonRepository, _mapper) 
                    = (logger, salesOrderRepository, salesPersonRepository, mapper);

        public async Task<SalesOrderDto> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting sales order from database");
            var spec = new GetSalesOrderSpecification(request.SalesOrder.SalesOrderNumber);
            var salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.Null(salesOrder, _logger);

            _logger.LogInformation("Updating sales order");
            _mapper.Map(request.SalesOrder, salesOrder);

            var salesPerson = await _salesPersonRepository.SingleOrDefaultAsync(
                new GetSalesPersonSpecification(
                    request.SalesOrder.SalesPerson.Name.FirstName,
                    request.SalesOrder.SalesPerson.Name.MiddleName,
                    request.SalesOrder.SalesPerson.Name.LastName
                ),
                cancellationToken
            );
            Guard.Against.Null(salesPerson, _logger);
            if (salesOrder.SalesPerson != salesPerson)
                salesOrder.SetSalesPerson(salesPerson);

            _logger.LogInformation("Saving sales order to database");
            await _salesOrderRepository.UpdateAsync(salesOrder, cancellationToken);

            _logger.LogInformation("Returning sales order");
            return _mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}