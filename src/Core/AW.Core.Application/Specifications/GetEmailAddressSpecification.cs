using Ardalis.Specification;
using AW.Core.Domain.Person;

namespace AW.Core.Application.Specifications
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