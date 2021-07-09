using System;

namespace AW.Services.Product.Core.Entities
{
    public class Illustration
    {
        public virtual int Id { get; protected set; }
        public string Diagram { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}