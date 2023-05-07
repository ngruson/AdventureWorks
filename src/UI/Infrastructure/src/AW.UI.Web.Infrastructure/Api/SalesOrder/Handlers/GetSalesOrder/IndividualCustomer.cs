using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder
{
    public class IndividualCustomer : Customer
    {
        public override string? CustomerName => Name?.FullName;
        public NameFactory? Name { get; set; }
    }
}
