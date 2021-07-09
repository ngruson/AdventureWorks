using AW.SharedKernel.AutoMapper;
using m = AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonPhone : IMapFrom<m.SalesPersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}