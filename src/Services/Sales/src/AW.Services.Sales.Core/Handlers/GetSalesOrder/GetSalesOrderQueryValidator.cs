using AW.Services.Sales.Core.Specifications;
using AW.SharedKernel.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;


namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQueryValidator : AbstractValidator<GetSalesOrderQuery>
    {
        private readonly IRepository<Entities.SalesOrder> salesOrderRepository;

        public GetSalesOrderQueryValidator(IRepository<Entities.SalesOrder> salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;

            RuleFor(cmd => cmd.SalesOrderNumber)
                .NotEmpty().WithMessage("Sales order number is required")
                .MaximumLength(25).WithMessage("Sales order number must not exceed 25 characters")
                .MustAsync(SalesOrderExists).WithMessage("Sales order does not exist");
        }

        private async Task<bool> SalesOrderExists(string salesOrderNumber, CancellationToken cancellationToken)
        {
            var salesOrder = await salesOrderRepository.GetBySpecAsync(
                new GetFullSalesOrderSpecification(salesOrderNumber),
                cancellationToken
            );

            return salesOrder != null;
        }
    }
}