using AW.UI.Web.Internal.Common;
using m = AW.UI.Web.Internal.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonEmailAddress : IMapFrom<m.SalesPersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}