using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class ProductProductPhotoDto
    {
        public int ProductId { get; set; }
        public int ProductPhotoId { get; set; }
        public bool Primary { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ProductDto Product { get; set; }
        public virtual ProductPhotoDto ProductPhoto { get; set; }
        public virtual ProductSubcategory ProductSubcategorys { get; set; }
        public virtual UnitMeasure SizeUnitMeasureCodeNavigation { get; set; }
        public virtual UnitMeasure WeightUnitMeasureCodeNavigation { get; set; }

    }
}
