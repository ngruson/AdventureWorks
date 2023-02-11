using AW.Services.Sales.Core.Entities;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class SalesPersonDto : IMapFrom<SalesPerson>
    {
        public NameFactory? Name { get; set; }
    }
}