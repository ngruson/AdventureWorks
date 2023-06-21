using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomers;

public class PersonEmailAddress : IMapFrom<Entities.PersonEmailAddress>
{
    public string? EmailAddress { get; set; }
}
