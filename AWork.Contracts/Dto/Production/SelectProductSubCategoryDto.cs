using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class SelectProductSubCategoryDto
    {
       public IEnumerable<ProductCategoryDto> productCategoryDto { get; set; }
       public IEnumerable<ProductSubCategoryDto>  productSubCategoryDto { get; set; }
    }
}
