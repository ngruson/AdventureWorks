using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class SalesPerson : IMapFrom<Entities.SalesPerson>
    {
        public NameFactory? Name { get; set; }
    }
}
