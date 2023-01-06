using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class ProSubCatDto
    {
        public IEnumerable<ProductCategoryDto> productCategoryDto { get; set; }
        public IEnumerable<ProductSubCategoryDto> productSubCategoryDto { get; set; }
        public IEnumerable<ProductDto> productDtos { get; set; }
    }
}
