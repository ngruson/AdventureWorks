using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomerDto, IndividualCustomer>();
        }
    }
}