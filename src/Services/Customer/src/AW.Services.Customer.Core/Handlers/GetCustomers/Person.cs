using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Handlers.GetCustomers;

public class Person : IMapFrom<Entities.Person>
{
    public string? Title { get; set; }
    public NameFactory? Name { get; set; }
    public string? Suffix { get; set; }
    public List<PersonEmailAddress> EmailAddresses { get; set; } = new();
}
