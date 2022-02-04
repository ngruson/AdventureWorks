using AW.Services.Infrastructure;

namespace AW.Services.Customer.Core.Exceptions
{
    public class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(string accountNumber)
            : base($"Customer {accountNumber} not found")
        { }
    }
}