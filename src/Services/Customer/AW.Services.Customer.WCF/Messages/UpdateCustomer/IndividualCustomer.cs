using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.Customer.Application.UpdateCustomer;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomer, IndividualCustomerDto>()
                .ReverseMap();
        }
    }
}