using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class CreditCardBuilder
    {
        private Domain.Sales.CreditCard creditCard = new Domain.Sales.CreditCard();

        public CreditCardBuilder Id(int id)
        {
            creditCard.Id = id;
            return this;
        }

        public CreditCardBuilder CardType(string cardType)
        {
            creditCard.CardType = cardType;
            return this;
        }

        public CreditCardBuilder CardNumber(string cardNumber)
        {
            creditCard.CardNumber = cardNumber;
            return this;
        }

        public CreditCardBuilder ExpMonth(byte expMonth)
        {
            creditCard.ExpMonth = expMonth;
            return this;
        }

        public CreditCardBuilder ExpYear(byte expYear)
        {
            creditCard.ExpYear = expYear;
            return this;
        }

        public Domain.Sales.CreditCard Build()
        {
            return creditCard;
        }

        public CreditCardBuilder WithTestValues()
        {
            creditCard = new Domain.Sales.CreditCard
            {
                Id = new Random().Next(),
                CardType = "SuperiorCard",
                CardNumber = "33332664695310",
                ExpMonth = 11,
                ExpYear = 2006
            };

            return this;
        }
    }
}