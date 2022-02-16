using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetCreditCardSpecification : Specification<Entities.CreditCard>, ISingleResultSpecification
    {
        public GetCreditCardSpecification(string cardNumber)
        {
            Query.Where(a => a.CardNumber == cardNumber);
        }
    }
}