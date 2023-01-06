using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWork.Contracts.Dto.Production
{
    public class ProductSubCategoryDto
    {
        public int ProductSubcategoryId { get; set; }
        [ForeignKey("ProductCategoryDto")]
        public int ProductCategoryId { get; set; }
        [Required(ErrorMessage = "You must insert SubCategory Name")]
        public string Name { get; set; }
        public Guid Rowguid { get; set; } = new Guid();
        public DateTime ModifiedDate { get; set; } = DateTime.Now;


        public virtual ProductCategoryDto ProductCategory { get; set; }
        public virtual ICollection<ProductDto> Products { get; set; }
        public virtual ICollection<ProductProductPhotoDto> ProductProductPhotos { get; set; }


    }
}
