using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        public override CustomerType CustomerType { get; set; } = CustomerType.Individual;
        
        public Person? Person { get; set; }
    }
}
