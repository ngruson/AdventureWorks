using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.Core.Models.GetCustomers
{
    public class IndividualCustomer : Customer, IMapFrom<IndividualCustomerDto>
    {
        public Person Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomerDto, IndividualCustomer>();
        }
    }
}