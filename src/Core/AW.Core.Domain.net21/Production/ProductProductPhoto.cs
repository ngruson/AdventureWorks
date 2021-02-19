using System;

namespace AW.Core.Domain.Production
{
    public class ProductProductPhoto
    {
        public int ProductID { get; set; }

        public int ProductPhotoID { get; set; }

        public bool Primary { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ProductPhoto ProductPhoto { get; set; }
    }
}