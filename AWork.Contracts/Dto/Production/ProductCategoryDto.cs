using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.Production
{
    public class ProductCategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; } = new Guid();
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public virtual ICollection<ProductSubCategoryDto> ProductSubcategories { get; set; }

        public virtual List<ProductSubCategoryDto> ProductSubCategoryDto { get; set; }
        public virtual List<ProductSubCategoryForCreateDto> ProductSubCategoryForCreateDto { get; set; } = new List<ProductSubCategoryForCreateDto>();
    }
}
