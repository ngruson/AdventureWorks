using AW.UI.Web.Internal.Common;
using m = AW.UI.Web.Internal.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonPhone : IMapFrom<m.SalesPersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}