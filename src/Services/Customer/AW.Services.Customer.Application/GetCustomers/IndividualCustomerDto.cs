using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Domain.PersonCustomer>
    {
        public PersonDto Person { get; set; }
    }
}