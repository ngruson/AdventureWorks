using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class SpecialOfferBuilder
    {
        private Domain.Sales.SpecialOffer specialOffer = new Domain.Sales.SpecialOffer();

        public SpecialOfferBuilder Id(int id)
        {
            specialOffer.Id = id;
            return this;
        }

        public SpecialOfferBuilder Description(string description)
        {
            specialOffer.Description = description;
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

        public Domain.Sales.SpecialOffer Build()
        {
            return specialOffer;
        }

        public SpecialOfferBuilder WithTestValues()
        {
            specialOffer = new Domain.Sales.SpecialOffer
            {
                Id = 1,
                Description = "No Discount",
                Type = "No Discount",
                Category = "No Discount",
                StartDate = new DateTime(2011, 05, 01),
                EndDate = new DateTime(2014,11, 30),
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            return this;
        }
    }
}