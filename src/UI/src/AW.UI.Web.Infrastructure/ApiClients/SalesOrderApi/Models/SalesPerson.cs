using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models
{
    public class SalesPerson : IMapFrom<SalesPersonApi.Models.SalesPerson>
    {
        public NameFactory Name { get; set; }
    }
}