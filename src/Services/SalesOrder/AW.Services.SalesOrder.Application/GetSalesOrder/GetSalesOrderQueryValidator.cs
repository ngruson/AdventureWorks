using Ardalis.Specification;
using AW.Services.SalesOrder.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;


namespace AW.Services.SalesOrder.Application.GetSalesOrder
{
    public class GetSalesOrderQueryValidator : AbstractValidator<GetSalesOrderQuery>
    {
        private readonly IRepositoryBase<Domain.SalesOrder> salesOrderRepository;

        public GetSalesOrderQueryValidator(IRepositoryBase<Domain.SalesOrder> salesOrderRepository)
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