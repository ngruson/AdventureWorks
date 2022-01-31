using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class PersonEmailAddressDto : IMapFrom<Entities.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}