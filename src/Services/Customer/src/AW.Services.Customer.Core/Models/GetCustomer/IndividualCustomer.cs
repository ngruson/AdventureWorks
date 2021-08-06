using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Models.GetCustomer
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