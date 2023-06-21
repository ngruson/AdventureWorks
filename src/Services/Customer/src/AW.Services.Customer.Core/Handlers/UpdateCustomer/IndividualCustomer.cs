using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        private CustomerType _customerType = CustomerType.Store;
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
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, IndividualCustomer>()
                .ReverseMap();
        }
    }
}
