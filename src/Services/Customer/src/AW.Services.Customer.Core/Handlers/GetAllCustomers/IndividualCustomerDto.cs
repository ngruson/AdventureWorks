using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public PersonDto Person { get; set; }
    }
}