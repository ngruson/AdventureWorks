using System;

namespace AW.Core.Domain.Sales
{
    public partial class Currency
    {
        public string CurrencyCode { get; set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}