using Ardalis.GuardClauses;
using AutoMapper;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, SalesOrderDto>
    {
        private readonly ILogger<UpdateSalesOrderCommandHandler> logger;
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;
        private readonly IRepository<Entities.SalesPerson> salesPersonRepository;
        private readonly IMapper mapper;

        public UpdateSalesOrderCommandHandler(
            ILogger<UpdateSalesOrderCommandHandler> logger,
            IRepository<Entities.SalesOrder> salesOrderRepository,
            IRepository<Entities.SalesPerson> salesPersonRepository,
            IMapper mapper) =>
                (this.logger, this.salesOrderRepository, this.salesPersonRepository, this.mapper) 
                    = (logger, salesOrderRepository, salesPersonRepository, mapper);

        public async Task<SalesOrderDto> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handle called");

            logger.LogInformation("Getting sales order from database");
            var spec = new GetSalesOrderSpecification(request.SalesOrder.SalesOrderNumber);
            var salesOrder = await salesOrderRepository.GetBySpecAsync(spec, cancellationToken);
            Guard.Against.Null(salesOrder, nameof(salesOrder));

            logger.LogInformation("Updating sales order");
            mapper.Map(request.SalesOrder, salesOrder);

            var salesPerson = await salesPersonRepository.GetBySpecAsync(
                new GetSalesPersonSpecification(
                    request.SalesOrder.SalesPerson.Name.FirstName,
                    request.SalesOrder.SalesPerson.Name.MiddleName,
                    request.SalesOrder.SalesPerson.Name.LastName
                ),
                cancellationToken
            );
            Guard.Against.Null(salesPerson, nameof(salesPerson));
            salesOrder.SetSalesPerson(salesPerson);

            logger.LogInformation("Saving sales order to database");
            await salesOrderRepository.UpdateAsync(salesOrder, cancellationToken);

            logger.LogInformation("Returning sales order");
            return mapper.Map<SalesOrderDto>(salesOrder);
        }
    }
}