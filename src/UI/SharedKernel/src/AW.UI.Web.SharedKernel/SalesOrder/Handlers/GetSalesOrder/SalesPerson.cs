using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrder
{
    public class SalesPerson : IMapFrom<SalesPerson>
    {
        public NameFactory? Name { get; set; }
    }
}