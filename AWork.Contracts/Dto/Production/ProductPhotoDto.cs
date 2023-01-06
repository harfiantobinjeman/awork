using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Production
{
    public class ProductPhotoDto
    {
        public int ProductPhotoId { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }

        public byte[] LargePhoto { get; set; }
        public string LargePhotoFileName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<ProductProductPhotoDto> ProductPhoto { get; set; }

    }
}
