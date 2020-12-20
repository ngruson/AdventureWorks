using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class SalesReasonBuilder
    {
        private Domain.Sales.SalesReason salesReason = new Domain.Sales.SalesReason();

        public Domain.Sales.SalesReason Build()
        {
            return salesReason;
        }

        public SalesReasonBuilder Id(int id)
        {
            salesReason.Id = id;
            return this;
        }

        public SalesReasonBuilder Name(string name)
        {
            salesReason.Name = name;
            return this;
        }

        public SalesReasonBuilder ReasonType(string reasonType)
        {
            salesReason.ReasonType = reasonType;
            return this;
        }

        public SalesReasonBuilder ModifiedDate(DateTime modifiedDate)
        {
            salesReason.ModifiedDate = modifiedDate;
            return this;
        }

        public SalesReasonBuilder WithTestValues()
        {
            salesReason = new Domain.Sales.SalesReason
            {
                Id = new Random().Next(),
                Name = "Price",
                ReasonType = "Other",
                ModifiedDate = DateTime.Now
            };

            return this;
        }
    }
}