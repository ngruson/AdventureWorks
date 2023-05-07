using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder
{
    public class SalesPerson : IMapFrom<GetSalesOrder.SalesPerson>
    {
        public NameFactory? Name { get; set; }
    }
}