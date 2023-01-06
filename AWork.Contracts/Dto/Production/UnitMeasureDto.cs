using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Production
{
    public class UnitMeasureDto
    {
        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public virtual ICollection<ProductDto> Products { get; set; }
        public virtual ICollection<ProductProductPhotoDto> ProductProductPhotos { get; set; }


    }
}
