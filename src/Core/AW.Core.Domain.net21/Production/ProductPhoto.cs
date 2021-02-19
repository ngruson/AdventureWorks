using System;

namespace AW.Core.Domain.Production
{

    public class ProductPhoto
    {
        public int Id { get; set; }
        public byte[] ThumbNailPhoto { get; set; }

        public string ThumbnailPhotoFileName { get; set; }

        public byte[] LargePhoto { get; set; }

        public string LargePhotoFileName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}