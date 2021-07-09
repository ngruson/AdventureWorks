using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<GetCustomer.CustomerAddress>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }
    }
}