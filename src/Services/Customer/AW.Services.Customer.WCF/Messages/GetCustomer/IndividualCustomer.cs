using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public PersonDto Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomerDto, IndividualCustomer>();
        }
    }
}