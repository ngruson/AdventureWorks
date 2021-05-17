using AW.Common.AutoMapper;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<GetCustomer.CustomerAddress>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}