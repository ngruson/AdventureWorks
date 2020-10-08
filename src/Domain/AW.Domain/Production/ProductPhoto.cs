using System;

namespace AW.Domain.Production
{

    public class ProductPhoto : BaseEntity
    {
        public byte[] ThumbNailPhoto { get; set; }

        public string ThumbnailPhotoFileName { get; set; }

        public byte[] LargePhoto { get; set; }

        public string LargePhotoFileName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}