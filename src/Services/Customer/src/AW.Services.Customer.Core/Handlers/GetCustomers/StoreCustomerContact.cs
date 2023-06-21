using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomers;

public class StoreCustomerContact : IMapFrom<Entities.StoreCustomerContact>
{
    public string? ContactType { get; set; }
    public Person? ContactPerson { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.StoreCustomerContact, StoreCustomerContact>();
    }
}
