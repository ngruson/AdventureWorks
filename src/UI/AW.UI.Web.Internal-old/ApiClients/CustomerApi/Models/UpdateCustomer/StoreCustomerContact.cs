using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<GetCustomer.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}