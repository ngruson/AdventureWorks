using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; } = new();
    }
}