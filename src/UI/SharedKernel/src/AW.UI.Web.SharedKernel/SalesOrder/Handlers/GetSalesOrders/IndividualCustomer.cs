using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders
{
    public class IndividualCustomer : Customer
    {
        public override string? CustomerName => Name?.FullName;
        public NameFactory? Name { get; set; }
    }
}