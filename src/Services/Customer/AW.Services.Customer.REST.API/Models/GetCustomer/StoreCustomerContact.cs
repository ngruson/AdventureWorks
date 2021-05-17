using AW.Common.AutoMapper;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }
    }
}