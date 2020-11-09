using System;

namespace AW.Application.UnitTests.TestBuilders
{
    public class CurrencyRateBuilder
    {
        private Domain.Sales.CurrencyRate currencyRate = new Domain.Sales.CurrencyRate();

        public CurrencyRateBuilder Id(int id)
        {
            currencyRate.Id = id;
            return this;
        }

        public CurrencyRateBuilder CurrencyRateDate(DateTime currencyRateDate)
        {
            currencyRate.CurrencyRateDate = currencyRateDate;
            return this;
        }

        public CurrencyRateBuilder FromCurrency(Domain.Sales.Currency currency)
        {
            currencyRate.FromCurrency = currency;
            return this;
        }

        public CurrencyRateBuilder ToCurrency(Domain.Sales.Currency currency)
        {
            currencyRate.ToCurrency = currency;
            return this;
        }

        public CurrencyRateBuilder AverageRate(decimal averageRate)
        {
            currencyRate.AverageRate = averageRate;
            return this;
        }

        public CurrencyRateBuilder EndOfDayRate(decimal endOfDayRate)
        {
            currencyRate.EndOfDayRate = endOfDayRate;
            return this;
        }

        public Domain.Sales.CurrencyRate Build()
        {
            return currencyRate;
        }

        public CurrencyRateBuilder WithTestValues()
        {
            currencyRate = new Domain.Sales.CurrencyRate
            {
                Id = new Random().Next(),
                CurrencyRateDate = new DateTime(2011, 05, 31),
                FromCurrency = new Domain.Sales.Currency { CurrencyCode = "USD" },
                ToCurrency = new Domain.Sales.Currency { CurrencyCode = "ARS" },
                AverageRate = 1.00M,
                EndOfDayRate = 1.0002M
            };

            return this;
        }
    }
}