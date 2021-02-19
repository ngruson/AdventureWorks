using System;

namespace AW.Services.Product.Domain
{
    public class ProductReview
    {
        public virtual int Id { get; protected set; }

        public int ProductID { get; set; }

        public string ReviewerName { get; set; }

        public DateTime ReviewDate { get; set; }

        public string EmailAddress { get; set; }

        public int Rating { get; set; }

        public string Comments { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
    }
}