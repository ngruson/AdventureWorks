using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder
{
    public class IndividualCustomer : Customer, IMapFrom<GetSalesOrder.IndividualCustomer>
    {
        public override string? CustomerName => Name?.FullName;
        public NameFactory? Name { get; set; }
    }
}