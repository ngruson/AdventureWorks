using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}