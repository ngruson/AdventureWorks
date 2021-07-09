using AW.SharedKernel.AutoMapper;
using m = AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonEmailAddress : IMapFrom<m.SalesPersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}