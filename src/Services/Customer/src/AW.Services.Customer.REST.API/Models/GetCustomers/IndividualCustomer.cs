using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomers
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; }
    }
}