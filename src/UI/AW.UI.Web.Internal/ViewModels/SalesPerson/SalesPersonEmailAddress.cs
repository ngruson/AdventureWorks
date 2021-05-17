using AW.Common.AutoMapper;
using m = AW.UI.Web.Common.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonEmailAddress : IMapFrom<m.SalesPersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}