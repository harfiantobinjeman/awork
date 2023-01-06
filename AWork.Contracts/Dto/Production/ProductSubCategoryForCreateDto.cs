using System;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Production
{
    public class ProductSubCategoryForCreateDto
    {
        public int ProductCategoryId { get; set; }

       
        public string Name { get; set; }
        public Guid Rowguid { get; set; } = new Guid();
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual ProductCategoryDto ProductCategory { get; set; }
    }
}
