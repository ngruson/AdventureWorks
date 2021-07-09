using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}