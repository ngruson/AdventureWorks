using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomer, IndividualCustomerDto>()
                .ForMember(m => m.Person, opt => opt.MapFrom(src => src.Person))
                .ReverseMap();
        }
    }
}