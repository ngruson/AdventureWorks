using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public abstract class Customer : ICustomer
    {
        public CustomerType CustomerType { get; set; }
        public string? Territory { get; set; }
        public List<CustomerAddress?>? Addresses { get; set; }
    }
}