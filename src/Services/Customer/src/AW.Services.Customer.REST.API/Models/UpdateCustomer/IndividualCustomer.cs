using AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.REST.API.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomer, IndividualCustomerDto>()
                .ForMember(m => m.Person, opt => opt.MapFrom(src => src.Person))
                .ReverseMap();
        }
    }
}