using System;

namespace AW.Services.Product.Domain
{
    public class Culture
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}