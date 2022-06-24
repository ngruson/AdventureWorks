using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPerson
{
    public class SalesPerson
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public string? Territory { get; set; }
        public List<SalesPersonEmailAddress>? EmailAddresses { get; set; }
        public List<SalesPersonPhone>? PhoneNumbers { get; set; }
    }
}