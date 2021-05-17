using AW.Common.AutoMapper;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<GetCustomer.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}