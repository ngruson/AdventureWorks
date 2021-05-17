using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public PersonDto Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.IndividualCustomer, IndividualCustomerDto>();
        }
    }
}