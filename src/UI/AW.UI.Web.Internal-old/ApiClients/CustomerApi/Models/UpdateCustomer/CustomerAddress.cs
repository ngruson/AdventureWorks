using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<GetCustomer.CustomerAddress>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}