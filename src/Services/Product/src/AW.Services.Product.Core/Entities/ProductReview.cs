using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductReview
    {
        private int Id { get; set; }

        private int ProductID { get; set; }

        public string ReviewerName { get; private set; }

        public DateTime ReviewDate { get; private set; }

        public string EmailAddress { get; private set; }

        public int Rating { get; private set; }

        public string Comments { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public Product Product { get; private set; }
    }
}