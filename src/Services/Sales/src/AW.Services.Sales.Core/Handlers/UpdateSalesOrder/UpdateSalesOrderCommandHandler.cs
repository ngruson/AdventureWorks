using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Guards;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, SalesOrderDto>
    {
        private readonly ILogger<UpdateSalesOrderCommandHandler> _logger;
        private readonly IRepository<Entities.SalesOrder> _salesOrderRepository;
        private readonly IRepository<Entities.SalesPerson> _salesPersonRepository;
        private readonly IRepository<Entities.SpecialOfferProduct> _specialOfferProductRepository;
        private readonly IMapper _mapper;

        public UpdateSalesOrderCommandHandler(
            ILogger<UpdateSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository,
            IRepository<Entities.SalesPerson> salesPersonRepository,
            IRepository<Entities.SpecialOfferProduct> specialOfferProductRepository,
            IMapper mapper) =>
                (_logger, _salesOrderRepository, _salesPersonRepository, _mapper, _specialOfferProductRepository) 
                    = (logger, salesOrderRepository, salesPersonRepository, mapper, specialOfferProductRepository);

        public async Task<SalesOrderDto> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Getting sales order from database");
            var spec = new GetSalesOrderSpecification(request.SalesOrder!.SalesOrderNumber!);
            var salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(spec, cancellationToken);
            Guard.Against.SalesOrderNull(salesOrder, request.SalesOrder.SalesOrderNumber!, _logger);

            _logger.LogInformation("Updating sales order");
            _mapper.Map(request.SalesOrder, salesOrder);

            foreach (var orderLine in request.SalesOrder.OrderLines!)
            {
                var specialOfferSpec = new GetSpecialOfferProductSpecification(orderLine.ProductNumber!, orderLine.SpecialOfferDescription);
                var specialOfferProduct = await _specialOfferProductRepository.FirstOrDefaultAsync(specialOfferSpec);
                Guard.Against.SpecialOfferProductNull(specialOfferProduct, orderLine.ProductNumber!, _logger);

                var existingOrderLine = salesOrder!.OrderLines.SingleOrDefault(_ => _.ProductNumber == orderLine.ProductNumber);
                Guard.Against.SalesOrderLineNull(existingOrderLine, orderLine.ProductNumber!, _logger);

                existingOrderLine!.SpecialOfferProduct = specialOfferProduct!;
                existingOrderLine.SpecialOfferProductId = specialOfferProduct!.Id;
            }

            if (request.SalesOrder.SalesPerson != null)
            {
                var salesPerson = await _salesPersonRepository.SingleOrDefaultAsync(
                new GetSalesPersonSpecification(
                    request.SalesOrder.SalesPerson.Name!.FirstName!,
                    request.SalesOrder.SalesPerson.Name.MiddleName,
                    request.SalesOrder.SalesPerson.Name.LastName!
                ),
                cancellationToken
            );
                Guard.Against.SalesPersonNull(salesPerson, request.SalesOrder.SalesPerson.Name!.FullName!, _logger);
                if (salesOrder!.SalesPerson != salesPerson)
                    salesOrder.SetSalesPerson(salesPerson!);
            }

            _logger.LogInformation("Saving sales order to database");
            await _salesOrderRepository.UpdateAsync(salesOrder!, cancellationToken);

            _logger.LogInformation("Returning sales order");
            return _mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}
