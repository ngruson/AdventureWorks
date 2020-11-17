using Ardalis.Specification;
using AW.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.SalesOrder.GetSalesOrder
{
    public class GetSalesOrderQueryValidator : AbstractValidator<GetSalesOrderQuery>
    {
        private readonly IRepositoryBase<Domain.Sales.SalesOrderHeader> salesOrderRepository;

        public GetSalesOrderQueryValidator(IRepositoryBase<Domain.Sales.SalesOrderHeader> salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;

            RuleFor(cmd => cmd.SalesOrderNumber)
                .NotEmpty().WithMessage("Sales order number is required")
                .MaximumLength(25).WithMessage("Sales order number must not exceed 25 characters")
                .MustAsync(SalesOrderExists).WithMessage("Sales order does not exist");
        }

        private async Task<bool> SalesOrderExists(string salesOrderNumber, CancellationToken cancellationToken)
        {
            var salesOrder = await salesOrderRepository.GetBySpecAsync(new GetSalesOrderSpecification(salesOrderNumber));
            return salesOrder != null;
        }
    }
}