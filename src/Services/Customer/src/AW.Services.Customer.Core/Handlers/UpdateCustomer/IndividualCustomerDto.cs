using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Entities.IndividualCustomer>
    {
        public PersonDto Person { get; set; } = new PersonDto();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, IndividualCustomerDto>()
                .ReverseMap();
        }
    }
}