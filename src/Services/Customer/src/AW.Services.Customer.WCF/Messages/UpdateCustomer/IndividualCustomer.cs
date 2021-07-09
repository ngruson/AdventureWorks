using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;

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