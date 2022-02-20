using System;

namespace AW.Services.Product.Core.Entities
{
    public class Culture
    {
        private int Id { get; set; }
        public string Name { get; private set; }

        public DateTime ModifiedDate { get; private set; }        
    }
}