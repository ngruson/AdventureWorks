using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public PersonDto Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomer, IndividualCustomerDto>();
        }
    }
}