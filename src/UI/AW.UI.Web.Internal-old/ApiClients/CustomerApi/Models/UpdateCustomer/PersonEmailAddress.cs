using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<GetCustomer.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}