using System;

namespace AW.Domain.Sales
{
    public partial class CurrencyRate
    {
        public int CurrencyRateID { get; set; }

        public DateTime CurrencyRateDate { get; set; }

        public string FromCurrencyCode { get; set; }

        public string ToCurrencyCode { get; set; }

        public decimal AverageRate { get; set; }

        public decimal EndOfDayRate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Currency FromCurrency { get; set; }

        public virtual Currency ToCurrency { get; set; }
    }
}