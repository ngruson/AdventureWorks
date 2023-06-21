using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;

public class Person
{
    public Guid ObjectId { get; set; }
    public string? Title { get; set; }
    public NameFactory? Name { get; set; }
    public string? Suffix { get; set; }
    public List<PersonEmailAddress>? EmailAddresses { get; set; }
    public List<PersonPhone>? PhoneNumbers { get; set; }
}
