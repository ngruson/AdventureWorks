using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.AddCustomer
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Entities.IndividualCustomer>
    {
        public PersonDto Person { get; set; } = new PersonDto();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomerDto, Entities.IndividualCustomer>()
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}