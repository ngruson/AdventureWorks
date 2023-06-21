using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        private CustomerType _customerType = CustomerType.Individual;
        public override CustomerType CustomerType
        {
            get
            {
                return _customerType;
            }
            set
            {
                _customerType = value;
            }

        }
        public Person? Person { get; set; }
    }
}
