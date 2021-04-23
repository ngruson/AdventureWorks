using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class PersonPhone : IMapFrom<GetCustomer.PersonPhone>
    {
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
    }
}