using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<GetCustomer.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}