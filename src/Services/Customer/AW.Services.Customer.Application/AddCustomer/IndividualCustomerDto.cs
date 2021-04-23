using AutoMapper;
using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.AddCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Domain.IndividualCustomer>
    {
        public PersonDto Person { get; set; } = new PersonDto();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.IndividualCustomer, IndividualCustomerDto>();
        }
    }
}