using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders
{
    public class SalesPerson : IMapFrom<SalesPerson>
    {
        public NameFactory? Name { get; set; }
    }
}