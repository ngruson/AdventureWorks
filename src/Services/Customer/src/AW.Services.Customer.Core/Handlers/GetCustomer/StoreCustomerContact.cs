using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class StoreCustomerContact : IMapFrom<Entities.StoreCustomerContact>
    {
        public Guid ObjectId { get; set; }
        public string? ContactType { get; set; }
        public Person? ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomerContact, StoreCustomerContact>();
        }
    }
}
