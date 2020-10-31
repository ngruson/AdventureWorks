using Ardalis.Specification;
using AW.Domain.Person;

namespace AW.Application.Specifications
{
    public class GetEmailAddressSpecification : Specification<EmailAddress>
    {
        public GetEmailAddressSpecification(string emailAddress) : base()
        {
            Query.
                Where(e => e.EmailAddress1 == emailAddress);
        }
    }
}