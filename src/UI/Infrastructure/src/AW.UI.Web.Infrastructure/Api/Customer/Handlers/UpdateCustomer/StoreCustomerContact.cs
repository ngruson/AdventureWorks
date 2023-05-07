using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class StoreCustomerContact : IMapFrom<GetCustomer.StoreCustomerContact>
    {
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.StoreCustomerContact, StoreCustomerContact>();
            profile.CreateMap<GetStoreCustomer.StoreCustomerContact, StoreCustomerContact>();
        }
    }
}