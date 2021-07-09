using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.GetCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; }
    }
}