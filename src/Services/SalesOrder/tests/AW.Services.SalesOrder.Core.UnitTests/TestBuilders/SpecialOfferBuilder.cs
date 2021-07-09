using AW.Services.SalesOrder.Core.Entities;
using System;

namespace AW.Services.SalesOrder.Core.UnitTests.TestBuilders
{
    public class SpecialOfferBuilder
    {
        private SpecialOffer specialOffer = new();

        public SpecialOfferBuilder Description(string description)
        {
            specialOffer.Description = description;
            return this;
        }

        public SpecialOfferBuilder DiscountPct(decimal discountPct)
        {
            specialOffer.DiscountPct = discountPct;
            return this;
        }

        public SpecialOfferBuilder Type(string type)
        {
            specialOffer.Type = type;
            return this;
        }

        public SpecialOfferBuilder Category(string category)
        {
            specialOffer.Category = category;
            return this;
        }

        public SpecialOfferBuilder StartDate(DateTime startDate)
        {
            specialOffer.StartDate = startDate;
            return this;
        }

        public SpecialOfferBuilder EndDate(DateTime endDate)
        {
            specialOffer.EndDate = endDate;
            return this;
        }

        public SpecialOfferBuilder MinQty(int minQty)
        {
            specialOffer.MinQty = minQty;
            return this;
        }

        public SpecialOfferBuilder MaxQty(int maxQty)
        {
            specialOffer.MaxQty = maxQty;
            return this;
        }

        public SpecialOffer Build()
        {
            return specialOffer;
        }

        public SpecialOfferBuilder WithTestValues()
        {
            specialOffer = new SpecialOffer
            {
                Description = "No Discount",
                DiscountPct = 0,
                Type = "No Discount",
                Category = "No Discount",
                StartDate = new DateTime(2021, 05, 01),
                EndDate = new DateTime(2024, 11, 30)
            };

            return this;
        }
    }
}