using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Domain.IndividualCustomer>
    {
        public PersonDto Person { get; set; }
    }
}